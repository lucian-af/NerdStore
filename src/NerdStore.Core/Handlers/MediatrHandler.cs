using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Interfaces;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Handlers
{
	public class MediatrHandler : IMediatrHandler
	{
		private readonly IMediator _mediator;

		public MediatrHandler(IMediator mediator)
			=> _mediator = mediator;

		public async Task PublicarEvento<T>(T evento) where T : Event
			=> await _mediator.Publish(evento);
	}
}
