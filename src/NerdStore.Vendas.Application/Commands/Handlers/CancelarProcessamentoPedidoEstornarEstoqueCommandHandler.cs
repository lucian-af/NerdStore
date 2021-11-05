using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Dtos;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class CancelarProcessamentoPedidoEstornarEstoqueCommandHandler :
		CommandHandler,
		IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public CancelarProcessamentoPedidoEstornarEstoqueCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;


		public async Task<bool> Handle(
			CancelarProcessamentoPedidoEstornarEstoqueCommand message,
			CancellationToken cancellationToken)
		{
			var pedido = await _repoPedido.ObterPorIdAsync(message.IdPedido);

			if (pedido == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
				return false;
			}

			var itensList = new List<ProdutoPedido>();
			pedido.PedidoItems.ForEach(i => itensList.Add(new ProdutoPedido { Id = i.IdProduto, Quantidade = i.Quantidade }));
			var listaProdutosPedido = new ListaProdutosPedido { IdPedido = pedido.Id, Itens = itensList };

			pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.IdCliente, listaProdutosPedido));
			pedido.TornarRascunho();

			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
