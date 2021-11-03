using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Settings;

namespace NerdStore.Catalogo.Domain.DomainServices
{
	public class EstoqueService : IEstoqueService
	{
		private readonly IProdutoRepository _repoProduto;
		private readonly IMediatorHandler _mediatrHandler;

		public EstoqueService(
			IProdutoRepository repoProduto,
			IMediatorHandler mediatrHandler)
		{
			_repoProduto = repoProduto;
			_mediatrHandler = mediatrHandler;
		}

		public async Task<bool> DebitarEstoque(Guid idProduto, int quantidade)
		{
			var produto = await _repoProduto.ObterPorIdAsync(idProduto);

			if (produto == null) return false;

			if (!produto.ExisteEstoque(quantidade))
				return false;

			produto.RetirarEstoque(quantidade);

			if (produto.QuantidadeEstoque < CatalogoSettings.QuantidadeMinimaEstoque)
				await _mediatrHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));

			_repoProduto.Atualizar(produto);

			return await _repoProduto.UnitOfWork.Commit();
		}

		public async Task<bool> ReporEstoque(Guid idProduto, int quantidade)
		{
			var produto = await _repoProduto.ObterPorIdAsync(idProduto);

			if (produto is null)
				return false;

			produto.AcrescentarEstoque(quantidade);

			_repoProduto.Atualizar(produto);

			return await _repoProduto.UnitOfWork.Commit();
		}

		public void Dispose()
		{
			_repoProduto.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
