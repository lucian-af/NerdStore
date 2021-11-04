using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class PedidoProdutoRemovidoEvent : Event
	{
		public Guid IdCliente { get; private set; }
		public Guid IdPedido { get; private set; }
		public Guid IdProduto { get; private set; }

		public PedidoProdutoRemovidoEvent(Guid idCliente, Guid idPedido, Guid idProduto)
		{
			IdAggregate = idPedido;
			IdCliente = idCliente;
			IdPedido = idPedido;
			IdProduto = idProduto;
		}
	}
}
