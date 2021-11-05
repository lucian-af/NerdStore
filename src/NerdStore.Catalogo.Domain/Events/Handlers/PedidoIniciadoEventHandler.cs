using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.IntegrationEvents;

namespace NerdStore.Catalogo.Domain.Events.Handlers
{
	public class PedidoIniciadoEventHandler : INotificationHandler<PedidoIniciadoEvent>
	{
		private readonly IEstoqueService _estoqueService;
		private readonly IMediatorHandler _mediatorHandler;

		public PedidoIniciadoEventHandler(
			IEstoqueService estoqueService,
			IMediatorHandler mediatorHandler)
		{
			_estoqueService = estoqueService;
			_mediatorHandler = mediatorHandler;
		}

		public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
		{
			var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);
			if (result)
			{
				await _mediatorHandler.PublicarEvento(
					new PedidoEstoqueConfirmadoEvent(
						message.IdCliente,
						message.IdPedido,
						message.Total,
						message.ProdutosPedido,
						message.NomeCartao,
						message.NumeroCartao,
						message.ExpiracaoCartao,
						message.CvvCartao
					)
				);
			}
			else
			{
				await _mediatorHandler.PublicarEvento(
					   new PedidoEstoqueRejeitadoEvent(message.IdPedido, message.IdCliente));
			}
		}
	}
}
