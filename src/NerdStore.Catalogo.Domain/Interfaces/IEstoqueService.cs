using System;
using System.Threading.Tasks;
using NerdStore.Core.Dtos;

namespace NerdStore.Catalogo.Domain.Interfaces
{
	public interface IEstoqueService : IDisposable
	{
		Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
		Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista);
		Task<bool> ReporEstoque(Guid produtoId, int quantidade);
		Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista);
	}
}
