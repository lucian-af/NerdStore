using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NerdStore.Vendas.Application.Queries.Dtos;
using NerdStore.Vendas.Application.Queries.Dtos.Interfaces;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Application.Queries
{
	public class PedidoQueries : IPedidoQueries
	{
		private readonly IPedidoRepository _pedidoRepository;

		public PedidoQueries(IPedidoRepository pedidoRepository)
			=> _pedidoRepository = pedidoRepository;

		public async Task<CarrinhoDto> ObterCarrinhoCliente(Guid idCliente)
		{
			var pedido = await _pedidoRepository.ObterPedidoRascunhoPorIdCliente(idCliente);

			if (pedido is null)
				return null;

			var carrinho = new CarrinhoDto
			{
				IdCliente = pedido.IdCliente,
				ValorTotal = pedido.ValorTotal,
				IdPedido = pedido.Id,
				ValorDesconto = pedido.Desconto,
				SubTotal = pedido.Desconto + pedido.ValorTotal
			};

			if (pedido.IdVoucher != null)
				carrinho.VoucherCodigo = pedido.Voucher.Codigo;

			foreach (var item in pedido.PedidoItems)
			{
				carrinho.Items.Add(new CarrinhoItemDto
				{
					IdProduto = item.IdProduto,
					NomeProduto = item.NomeProduto,
					Quantidade = item.Quantidade,
					ValorUnitario = item.ValorUnitario,
					ValorTotal = item.ValorUnitario * item.Quantidade
				});
			}

			return carrinho;
		}

		public async Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId)
		{
			var pedidos = await _pedidoRepository.ObterListaPorIdCliente(clienteId);

			pedidos = pedidos.Where(p => p.PedidoStatus == PedidoStatus.Pago || p.PedidoStatus == PedidoStatus.Cancelado)
				.OrderByDescending(p => p.Codigo);

			if (!pedidos.Any())
				return null;

			var pedidosView = new List<PedidoDto>();

			foreach (var pedido in pedidos)
			{
				pedidosView.Add(new PedidoDto
				{
					Id = pedido.Id,
					ValorTotal = pedido.ValorTotal,
					PedidoStatus = (int)pedido.PedidoStatus,
					Codigo = pedido.Codigo,
					DataCadastro = pedido.DataCadastro
				});
			}

			return pedidosView;
		}
	}
}
