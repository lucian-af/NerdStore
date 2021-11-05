using System;
using NerdStore.Core.Dtos;

namespace NerdStore.Core.Messages.Common.IntegrationEvents
{
	public class PedidoEstoqueConfirmadoEvent : IntegrationEvent
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }
		public decimal Total { get; private set; }
		public ListaProdutosPedido ProdutosPedido { get; private set; }
		public string NomeCartao { get; private set; }
		public string NumeroCartao { get; private set; }
		public string ExpiracaoCartao { get; private set; }
		public string CvvCartao { get; private set; }

		public PedidoEstoqueConfirmadoEvent(
			Guid idPedido,
			Guid idCliente,
			decimal total,
			ListaProdutosPedido produtosPedido,
			string nomeCartao,
			string numeroCartao,
			string expiracaoCartao,
			string cvvCartao)
		{
			IdAggregate = idPedido;
			IdPedido = idPedido;
			IdCliente = idCliente;
			Total = total;
			ProdutosPedido = produtosPedido;
			NomeCartao = nomeCartao;
			NumeroCartao = numeroCartao;
			ExpiracaoCartao = expiracaoCartao;
			CvvCartao = cvvCartao;
		}
	}
}
