﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Interfaces;

namespace NerdStore.Catalogo.Data.Repositories
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
		private readonly DbSet<Categoria> _dbSetCategoria;

		public ProdutoRepository(CatalogoContext context) : base(context)
			=> _dbSetCategoria = context.Set<Categoria>();

		public async Task<ICollection<Produto>> ObterPorCategoria(int codigo)
			=> await _dbSet.Include(c => c.Categoria).Where(c => c.Categoria.Codigo == codigo).ToListAsync();

		public async Task<ICollection<Categoria>> ObterCategorias()
			=> await _dbSetCategoria.ToListAsync();

		public async Task AdicionarCategoria(Categoria categoria)
			=> await _dbSetCategoria.AddAsync(categoria);
		public void AtualizarCategoria(Categoria categoria)
			=> _dbSetCategoria.Update(categoria);
	}
}