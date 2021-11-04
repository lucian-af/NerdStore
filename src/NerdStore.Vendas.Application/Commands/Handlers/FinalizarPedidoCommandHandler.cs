using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Events.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class FinalizarPedidoCommandHandler : CommandHandler, IRequestHandler<FinalizarPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public FinalizarPedidoCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(FinalizarPedidoCommand message, CancellationToken cancellationToken)
		{
			var pedido = await _repoPedido.ObterPorIdAsync(message.IdPedido);

			if (pedido == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
				return false;
			}

			pedido.FinalizarPedido();

			pedido.AdicionarEvento(new PedidoFinalizadoEvent(message.IdPedido));

			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
