using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class CancelarProcessamentoPedidoCommandHandler :
		CommandHandler,
		IRequestHandler<CancelarProcessamentoPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public CancelarProcessamentoPedidoCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(CancelarProcessamentoPedidoCommand message, CancellationToken cancellationToken)
		{
			var pedido = await _repoPedido.ObterPorIdAsync(message.IdPedido);

			if (pedido == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
				return false;
			}

			pedido.TornarRascunho();

			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
