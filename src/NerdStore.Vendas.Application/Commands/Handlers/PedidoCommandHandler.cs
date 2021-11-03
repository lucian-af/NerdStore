using System.Linq;
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
	public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;
		private readonly IMediatorHandler _mediatorHandler;

		public PedidoCommandHandler(
			IPedidoRepository repoPedido,
			IMediatorHandler mediatorHandler)
		{
			_repoPedido = repoPedido;
			_mediatorHandler = mediatorHandler;
		}

		public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
		{
			if (!ValidarComando(message))
				return false;

			var pedido = await _repoPedido.ObterPedidoRascunhoPorIdCliente(message.IdCliente);
			var pedidoItem = new PedidoItem(message.IdProduto, message.Nome, message.Quantidade, message.ValorUnitario);

			if (pedido is null)
			{
				pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.IdCliente);
				pedido.AdicionarItem(pedidoItem);

				await _repoPedido.AdicionarAsync(pedido);
			}
			else
			{
				pedido.AdicionarItem(pedidoItem);

				if (pedido.PedidoItemExistente(pedidoItem))
					_repoPedido.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.IdProduto == pedidoItem.IdProduto));
				else
					_repoPedido.AdicionarItem(pedidoItem);
			}

			return await _repoPedido.UnitOfWork.Commit();
		}


		/// <summary>
		/// Validar command com disparo de eventos
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private bool ValidarComando(Command message)
		{
			if (message.Valido())
				return true;

			message.ValidationResult.Errors
				.ForEach(erro => _mediatorHandler
				.PublicarNotificacao(new DomainNotification(message.MessageType, erro.ErrorMessage)));

			return false;
		}
	}
}
