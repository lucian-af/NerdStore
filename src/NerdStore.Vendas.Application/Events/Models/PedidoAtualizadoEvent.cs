using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class PedidoAtualizadoEvent : Event
	{
		public Guid IdCliente { get; private set; }
		public Guid IdPedido { get; private set; }
		public decimal ValorTotal { get; private set; }

		public PedidoAtualizadoEvent(Guid idCliente, Guid idPedido, decimal valorTotal)
		{
			IdAggregate = idPedido;
			IdCliente = idCliente;
			IdPedido = idPedido;
			ValorTotal = valorTotal;
		}
	}
}
