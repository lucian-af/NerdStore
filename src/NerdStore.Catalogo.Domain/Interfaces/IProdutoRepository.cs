using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Core.Data.Interfaces;

namespace NerdStore.Catalogo.Domain.Interfaces
{
	public interface IProdutoRepository : IRepository<Produto>
	{
		Task<ICollection<Produto>> ObterPorCategoria(int codigo);
		Task<ICollection<Categoria>> ObterCategorias();
		Task AdicionarCategoria(Categoria categoria);
		void AtualizarCategoria(Categoria categoria);
	}
}
