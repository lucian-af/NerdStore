using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class PedidoItemAdicionadoEvent : Event
	{
		public Guid IdCliente { get; private set; }
		public Guid IdPedido { get; private set; }
		public Guid IdProduto { get; private set; }
		public decimal ValorUnitario { get; private set; }
		public int Quantidade { get; private set; }

		public PedidoItemAdicionadoEvent(Guid idCliente, Guid idPedido, Guid idProduto, decimal valorUnitario, int quantidade)
		{
			IdAggregate = idPedido;
			IdCliente = idCliente;
			IdPedido = idPedido;
			IdProduto = idProduto;
			ValorUnitario = valorUnitario;
			Quantidade = quantidade;
		}
	}
}
