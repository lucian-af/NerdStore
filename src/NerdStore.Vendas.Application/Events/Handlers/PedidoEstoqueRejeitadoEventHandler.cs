using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Events
{
	public class PedidoEstoqueRejeitadoEventHandler : INotificationHandler<PedidoEstoqueRejeitadoEvent>
	{
		private readonly IMediatorHandler _mediatorHandler;

		public PedidoEstoqueRejeitadoEventHandler(IMediatorHandler mediatorHandler)
			=> _mediatorHandler = mediatorHandler;

		public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
			=> await _mediatorHandler.EnviarComando(
				 new CancelarProcessamentoPedidoCommand(message.IdPedido, message.IdCliente));
	}
}
