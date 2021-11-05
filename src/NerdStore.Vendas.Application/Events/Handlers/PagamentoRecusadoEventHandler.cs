using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Events
{
	public class PagamentoRecusadoEventHandler : INotificationHandler<PagamentoRecusadoEvent>
	{

		private readonly IMediatorHandler _mediatorHandler;

		public PagamentoRecusadoEventHandler(IMediatorHandler mediatorHandler)
			=> _mediatorHandler = mediatorHandler;

		public async Task Handle(PagamentoRecusadoEvent message, CancellationToken cancellationToken)
			=> await _mediatorHandler.EnviarComando(
				 new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.IdPedido, message.IdCliente));
	}
}
