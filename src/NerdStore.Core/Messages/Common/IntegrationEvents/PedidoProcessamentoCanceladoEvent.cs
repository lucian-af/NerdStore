using System;
using NerdStore.Core.Dtos;
using NerdStore.Core.Messages.Common.IntegrationEvents;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
	public class PedidoProcessamentoCanceladoEvent : IntegrationEvent
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }
		public ListaProdutosPedido ProdutosPedido { get; private set; }

		public PedidoProcessamentoCanceladoEvent(
			Guid idPedido,
			Guid idCliente,
			ListaProdutosPedido produtosPedido)
		{
			IdAggregate = idPedido;
			IdPedido = idPedido;
			IdCliente = idCliente;
			ProdutosPedido = produtosPedido;
		}
	}
}
