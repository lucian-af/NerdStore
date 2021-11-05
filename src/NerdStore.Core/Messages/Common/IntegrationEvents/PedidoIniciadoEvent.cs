using System;
using NerdStore.Core.Dtos;

namespace NerdStore.Core.Messages.Common.IntegrationEvents
{
	public class PedidoIniciadoEvent : IntegrationEvent
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }
		public decimal Total { get; private set; }
		public ListaProdutosPedido ProdutosPedido { get; private set; }
		public string NomeCartao { get; private set; }
		public string NumeroCartao { get; private set; }
		public string ExpiracaoCartao { get; private set; }
		public string CvvCartao { get; private set; }

		public PedidoIniciadoEvent(
			Guid idPedido,
			Guid idCliente,
			ListaProdutosPedido itens,
			decimal total,
			string nomeCartao,
			string numeroCartao,
			string expiracaoCartao,
			string cvvCartao)
		{
			IdAggregate = idPedido;
			IdPedido = idPedido;
			IdCliente = idCliente;
			ProdutosPedido = itens;
			Total = total;
			NomeCartao = nomeCartao;
			NumeroCartao = numeroCartao;
			ExpiracaoCartao = expiracaoCartao;
			CvvCartao = cvvCartao;
		}
	}
}
