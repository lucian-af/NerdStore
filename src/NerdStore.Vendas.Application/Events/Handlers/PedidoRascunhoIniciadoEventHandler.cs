using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Vendas.Application.Events.Models;

namespace NerdStore.Vendas.Application.Events.Handlers
{
	public class PedidoRascunhoIniciadoEventHandler : INotificationHandler<PedidoRascunhoIniciadoEvent>
	{
		public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
			=> Task.CompletedTask;
	}
}
