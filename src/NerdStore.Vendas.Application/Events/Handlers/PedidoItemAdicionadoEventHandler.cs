using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Vendas.Application.Events.Models;

namespace NerdStore.Vendas.Application.Events.Handlers
{
	public class PedidoItemAdicionadoEventHandler : INotificationHandler<PedidoItemAdicionadoEvent>
	{
		public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;
	}
}
