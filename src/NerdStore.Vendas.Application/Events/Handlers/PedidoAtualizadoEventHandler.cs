using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Vendas.Application.Events.Models;

namespace NerdStore.Vendas.Application.Events.Handlers
{
	public class PedidoAtualizadoEventHandler : INotificationHandler<PedidoAtualizadoEvent>
	{
		public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;
	}
}
