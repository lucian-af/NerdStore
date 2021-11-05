using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Events
{
	public class PagamentoRealizadoEventHandler : INotificationHandler<PagamentoRealizadoEvent>
	{
		private readonly IMediatorHandler _mediatorHandler;

		public PagamentoRealizadoEventHandler(IMediatorHandler mediatorHandler)
			=> _mediatorHandler = mediatorHandler;

		public async Task Handle(PagamentoRealizadoEvent message, CancellationToken cancellationToken)
			=> await _mediatorHandler.EnviarComando(
				 new FinalizarPedidoCommand(message.IdPedido, message.IdCliente));
	}
}
