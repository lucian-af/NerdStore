using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class PedidoFinalizadoEvent : Event
	{
		public Guid IdPedido { get; private set; }

		public PedidoFinalizadoEvent(Guid idPedido)
		{
			IdPedido = idPedido;
			IdAggregate = idPedido;
		}
	}
}
