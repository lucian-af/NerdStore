using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Events.Models;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Dtos;
using NerdStore.Core.Messages.Common.Notifications;

namespace NerdStore.Catalogo.Domain.DomainServices
{
	public class EstoqueService : IEstoqueService
	{
		private readonly IProdutoRepository _repoProduto;
		private readonly IMediatorHandler _mediatorHandler;

		public EstoqueService(
			IProdutoRepository repoProduto,
			IMediatorHandler mediatrHandler)
		{
			_repoProduto = repoProduto;
			_mediatorHandler = mediatrHandler;
		}

		public async Task<bool> DebitarEstoque(Guid idProduto, int quantidade)
		{
			if (!await DebitarItemEstoque(idProduto, quantidade))
				return false;

			return await _repoProduto.UnitOfWork.Commit();
		}

		private async Task<bool> DebitarItemEstoque(Guid idProduto, int quantidade)
		{
			var produto = await _repoProduto.ObterPorIdAsync(idProduto);

			if (produto == null)
				return false;

			if (!produto.ExisteEstoque(quantidade))
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
				return false;
			}

			produto.RetirarEstoque(quantidade);

			// TODO: 10 pode ser parametrizavel em arquivo de configuração
			if (produto.QuantidadeEstoque < 10)
			{
				await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
			}

			_repoProduto.Atualizar(produto);
			return true;
		}

		public async Task<bool> ReporEstoque(Guid idProduto, int quantidade)
		{
			var sucesso = await ReporItemEstoque(idProduto, quantidade);

			if (!sucesso) return false;

			return await _repoProduto.UnitOfWork.Commit();
		}

		private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
		{
			var produto = await _repoProduto.ObterPorIdAsync(produtoId);

			if (produto == null)
				return false;

			produto.AcrescentarEstoque(quantidade);

			_repoProduto.Atualizar(produto);

			return true;
		}

		public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
		{
			foreach (var item in lista.Itens)
			{
				if (!await DebitarItemEstoque(item.Id, item.Quantidade))
					return false;
			}

			return await _repoProduto.UnitOfWork.Commit();
		}

		public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
		{
			foreach (var item in lista.Itens)
			{
				await ReporItemEstoque(item.Id, item.Quantidade);
			}

			return await _repoProduto.UnitOfWork.Commit();
		}
		public void Dispose()
		{
			_repoProduto.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
