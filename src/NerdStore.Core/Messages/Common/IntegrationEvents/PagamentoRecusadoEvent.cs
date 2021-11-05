using System;

namespace NerdStore.Core.Messages.Common.IntegrationEvents
{
	public class PagamentoRecusadoEvent : IntegrationEvent
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }
		public Guid IdPagamento { get; private set; }
		public Guid IdTransacao { get; private set; }
		public decimal Total { get; private set; }

		public PagamentoRecusadoEvent(Guid idPedido, Guid idCliente, Guid idPagamento, Guid idTransacao, decimal total)
		{
			IdAggregate = idPagamento;
			IdPedido = idPedido;
			IdCliente = idCliente;
			IdPagamento = idPagamento;
			IdTransacao = idTransacao;
			Total = total;
		}
	}

}
