using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Dtos;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Pagamentos.Business.Interfaces;

namespace NerdStore.Pagamentos.Business.Events
{
	public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
	{
		private readonly IPagamentoService _pagamentoService;

		public PagamentoEventHandler(IPagamentoService pagamentoService)
			=> _pagamentoService = pagamentoService;

		public async Task Handle(PedidoEstoqueConfirmadoEvent message, CancellationToken cancellationToken)
		{
			var pagamentoPedido = new PagamentoPedido
			{
				IdPedido = message.IdPedido,
				IdCliente = message.IdCliente,
				Total = message.Total,
				NomeCartao = message.NomeCartao,
				NumeroCartao = message.NumeroCartao,
				ExpiracaoCartao = message.ExpiracaoCartao,
				CvvCartao = message.CvvCartao
			};

			await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
		}
	}
}
