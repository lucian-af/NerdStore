using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class IniciarPedidoCommandHandler : CommandHandler, IRequestHandler<IniciarPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public IniciarPedidoCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
		{
			if (!ValidarComando(message))
				return false;

			//var pedido = await _repoPedido.ObterPedidoRascunhoPorIdCliente(message.IdCliente);
			//pedido.IniciarPedido();

			//var itensList = new List<Item>();
			//pedido.PedidoItems.ForEach(i => itensList.Add(new Item { Id = i.IdProduto, Quantidade = i.Quantidade }));
			//var listaProdutosPedido = new ListaProdutosPedido { IdPedido = pedido.Id, Itens = itensList };

			//pedido.AdicionarEvento(
			//	new PedidoIniciadoEvent(
			//		pedido.Id,
			//		pedido.IdCliente,
			//		listaProdutosPedido,
			//		pedido.ValorTotal,
			//		message.NomeCartao,
			//		message.NumeroCartao,
			//		message.ExpiracaoCartao,
			//		message.CvvCartao));

			//_repoPedido.Atualizar(pedido);
			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
