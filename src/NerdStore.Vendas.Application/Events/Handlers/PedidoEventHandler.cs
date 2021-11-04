using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Vendas.Application.Events.Models;

namespace NerdStore.Vendas.Application.Events.Handlers
{
	public class PedidoEventHandler :
		INotificationHandler<PedidoRascunhoIniciadoEvent>,
		INotificationHandler<PedidoAtualizadoEvent>,
		INotificationHandler<PedidoItemAdicionadoEvent>
	{
		public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;

		public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;

		public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;
	}
}
