using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;
using NerdStore.Core.Interfaces;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Domain.Entidades
{
	public class Pedido : Entidade, IAggregateRoot
	{
		protected Pedido()
			=> _pedidoItems = new List<PedidoItem>();
		public Pedido(Guid idCliente, bool voucherUtilizado, decimal desconto, decimal valorTotal)
		{
			IdCliente = idCliente;
			VoucherUtilizado = voucherUtilizado;
			Desconto = desconto;
			ValorTotal = valorTotal;
			_pedidoItems = new List<PedidoItem>();
		}

		/// <summary>
		/// Classe dentro de uma classe, é chamada de "Classe aninhada"
		/// </summary>
		public static class PedidoFactory
		{
			public static Pedido NovoPedidoRascunho(Guid idCliente)
			{
				var pedido = new Pedido
				{
					IdCliente = idCliente,
				};

				pedido.TornarRascunho();
				return pedido;
			}
		}


		public int Codigo { get; private set; }
		public Guid IdCliente { get; private set; }
		public Guid? IdVoucher { get; private set; }
		public bool VoucherUtilizado { get; private set; }
		public decimal Desconto { get; private set; }
		public decimal ValorTotal { get; private set; }
		public DateTime DataCadastro { get; private set; }
		public PedidoStatus PedidoStatus { get; private set; }
		public Voucher Voucher { get; private set; }

		private readonly List<PedidoItem> _pedidoItems;
		public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;


		public ValidationResult AplicarVoucher(Voucher voucher)
		{
			var validationResult = voucher.ValidarSeAplicavel();

			if (!validationResult.IsValid)
				return validationResult;

			Voucher = voucher;
			VoucherUtilizado = true;
			Voucher.DebitarQuantidadeVoucher();
			CalcularValorPedido();

			return validationResult;
		}

		public void CalcularValorPedido()
		{
			ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
			CalcularValorTotalDesconto();
		}

		public void CalcularValorTotalDesconto()
		{
			if (!VoucherUtilizado)
				return;

			var desconto = Voucher.TipoDescontoVoucher switch
			{
				TipoDescontoVoucher.Porcentagem => CalcularValorDescontoPorcentagem(),
				TipoDescontoVoucher.Valor => Voucher.ValorDesconto ?? 0,
				_ => 0
			};

			var valorTotal = ValorTotal -= desconto;

			ValorTotal = valorTotal < 0 ? 0 : valorTotal;
			Desconto = desconto;
		}

		private decimal CalcularValorDescontoPorcentagem()
		{
			if (!Voucher.Percentual.HasValue)
				return 0;

			return ValorTotal * Voucher.Percentual.Value / 100;
		}

		public bool PedidoItemExistente(PedidoItem item)
			=> _pedidoItems.Any(p => p.IdProduto == item.IdProduto);

		public void AdicionarItem(PedidoItem item)
		{
			item.Validar();

			item.AssociarPedido(Id);

			if (PedidoItemExistente(item))
			{
				var itemExistente = _pedidoItems.FirstOrDefault(p => p.IdProduto == item.IdProduto);
				itemExistente.AdicionarUnidades(item.Quantidade);
				item = itemExistente;

				_pedidoItems.Remove(itemExistente);
			}

			item.CalcularValor();
			_pedidoItems.Add(item);

			CalcularValorPedido();
		}

		public void RemoverItem(PedidoItem item)
		{
			item.Validar();

			var itemExistente = PedidoItems.FirstOrDefault(p => p.IdProduto == item.IdProduto);

			if (itemExistente == null)
				throw new DomainException("O item não pertence ao pedido");

			_pedidoItems.Remove(itemExistente);

			CalcularValorPedido();
		}

		public void AtualizarItem(PedidoItem item)
		{
			item.Validar();

			item.AssociarPedido(Id);

			var itemExistente = PedidoItems.FirstOrDefault(p => p.IdProduto == item.IdProduto);

			if (itemExistente == null)
				throw new DomainException("O item não pertence ao pedido");

			_pedidoItems.Remove(itemExistente);
			_pedidoItems.Add(item);

			CalcularValorPedido();
		}

		public void AtualizarUnidades(PedidoItem item, int unidades)
		{
			item.AtualizarUnidades(unidades);
			AtualizarItem(item);
		}

		public void TornarRascunho()
			=> PedidoStatus = PedidoStatus.Rascunho;

		public void IniciarPedido()
			=> PedidoStatus = PedidoStatus.Iniciado;

		public void FinalizarPedido()
			=> PedidoStatus = PedidoStatus.Pago;

		public void CancelarPedido()
			=> PedidoStatus = PedidoStatus.Cancelado;
	}
}
