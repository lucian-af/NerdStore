using System;

namespace NerdStore.Core.Messages.Common.IntegrationEvents
{
	public class PedidoEstoqueRejeitadoEvent : IntegrationEvent
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }

		public PedidoEstoqueRejeitadoEvent(Guid idPedido, Guid idCliente)
		{
			IdAggregate = idPedido;
			IdPedido = idPedido;
			IdCliente = idCliente;
		}
	}
}
