using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Interfaces;

namespace NerdStore.Core.Data
{
	public abstract class Repository<TEntity, TContext> :
		IRepository<TEntity> where TEntity : Entidade, IAggregateRoot
		where TContext : DbContext, IUnitOfWork
	{
		private readonly TContext _context;
		protected readonly DbSet<TEntity> _dbSet;

		protected Repository(TContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}
		public IUnitOfWork UnitOfWork => _context;

		public virtual async Task AdicionarAsync(TEntity obj)
			=> await _dbSet.AddAsync(obj);

		public virtual void Atualizar(TEntity obj)
			=> _dbSet.Update(obj);

		public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
			=> await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

		public virtual async Task<ICollection<TEntity>> ObterTodosAsync()
			=> await _dbSet.AsNoTracking().ToListAsync();

		public virtual void Remover(TEntity obj)
			=> _dbSet.Remove(obj);

		public void Dispose()
			=> GC.SuppressFinalize(this);
	}
}
