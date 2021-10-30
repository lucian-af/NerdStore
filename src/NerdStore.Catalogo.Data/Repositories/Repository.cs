using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Interfaces;

namespace NerdStore.Catalogo.Data.Repositories
{
	public abstract class Repository<T> : IRepository<T> where T : Entidade, IAggregateRoot
	{
		private readonly CatalogoContext _context;
		protected readonly DbSet<T> _dbSet;

		protected Repository(CatalogoContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}
		public IUnitOfWork UnitOfWork => _context;

		public virtual async Task Adicionar(T obj)
			=> await _dbSet.AddAsync(obj);

		public virtual void Atualizar(T obj)
			=> _dbSet.Update(obj);

		public virtual async Task<T> ObterPorId(Guid id)
			=> await _dbSet.FindAsync(id);

		public virtual async Task<ICollection<T>> ObterTodos()
			=> await _dbSet.ToListAsync();

		public virtual void Remover(T obj)
			=> _dbSet.Remove(obj);

		public void Dispose()
			=> GC.SuppressFinalize(this);
	}
}
