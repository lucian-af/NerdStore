using System;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;
using NerdStore.Vendas.Domain.Validations;

namespace NerdStore.Vendas.Domain.Entidades
{
	public class PedidoItem : Entidade
	{
		protected PedidoItem() { }
		public PedidoItem(Guid idProduto, string nomeProduto, int quantidade, decimal valorUnitario)
		{
			IdProduto = idProduto;
			NomeProduto = nomeProduto;
			Quantidade = quantidade;
			ValorUnitario = valorUnitario;
		}


		public Guid IdPedido { get; private set; }
		public Guid IdProduto { get; private set; }
		public string NomeProduto { get; private set; }
		public int Quantidade { get; private set; }
		public decimal ValorUnitario { get; private set; }
		public Pedido Pedido { get; set; }


		internal void AssociarPedido(Guid idPedido)
			=> IdPedido = idPedido;

		public decimal CalcularValor()
			=> Quantidade * ValorUnitario;

		internal void AdicionarUnidades(int unidades)
			=> Quantidade += unidades;

		internal void AtualizarUnidades(int unidades)
			=> Quantidade = unidades;

		public override void Validar()
		{
			var result = new PedidoItemValidation().Validate(this);

			if (!result.IsValid)
				throw new DomainException(string.Join('|', result.Errors));
		}
	}
}
