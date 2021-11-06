using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Events.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class AdicionarItemPedidoCommandHandler : CommandHandler, IRequestHandler<AdicionarItemPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public AdicionarItemPedidoCommandHandler(
			IPedidoRepository repoPedido,
			IMediatorHandler mediatorHandler) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
		{
			if (!ValidarComando(message))
				return false;

			var pedido = await _repoPedido.ObterPedidoRascunhoPorIdCliente(message.IdCliente);
			var pedidoItem = new PedidoItem(message.IdProduto, message.Nome, message.Quantidade, message.ValorUnitario);

			if (pedido is null)
			{
				pedido = await NovoPedidoRascunho(message, pedidoItem);
			}
			else
			{
				AdicionarAtualizarPedidoItem(pedido, pedidoItem);
			}

			pedido.AdicionarEvento(
				new PedidoItemAdicionadoEvent(
					pedido.IdCliente,
					pedido.Id,
					message.IdProduto,
					message.ValorUnitario,
					message.Quantidade));

			return await _repoPedido.UnitOfWork.Commit();
		}

		private void AdicionarAtualizarPedidoItem(Pedido pedido, PedidoItem pedidoItem)
		{
			var pedidoItemExiste = pedido.PedidoItemExistente(pedidoItem);
			pedido.AdicionarItem(pedidoItem);

			if (pedidoItemExiste)
				_repoPedido.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.IdProduto == pedidoItem.IdProduto));
			else
				_repoPedido.AdicionarItem(pedidoItem);
		}

		private async Task<Pedido> NovoPedidoRascunho(AdicionarItemPedidoCommand message, PedidoItem pedidoItem)
		{
			var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.IdCliente);
			pedido.AdicionarItem(pedidoItem);

			await _repoPedido.AdicionarAsync(pedido);
			pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(message.IdCliente, pedido.Id));
			return pedido;
		}
	}
}
