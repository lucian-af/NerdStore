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
	public class RemoverItemPedidoCommandHandler : CommandHandler, IRequestHandler<RemoverItemPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public RemoverItemPedidoCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
		{
			if (!ValidarComando(message))
				return false;

			var pedido = await _repoPedido.ObterPedidoRascunhoPorIdCliente(message.IdCliente);

			if (pedido == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
				return false;
			}

			var pedidoItem = await _repoPedido.ObterItemPorPedido(pedido.Id, message.IdProduto);

			if (pedidoItem != null && !pedido.PedidoItemExistente(pedidoItem))
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
				return false;
			}

			pedido.RemoverItem(pedidoItem);
			pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.IdCliente, pedido.Id, pedido.ValorTotal));
			pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.IdCliente, pedido.Id, message.IdProduto));

			_repoPedido.RemoverItem(pedidoItem);
			_repoPedido.Atualizar(pedido);

			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
