using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Core.Interfaces;

namespace NerdStore.Core.Data.Interfaces
{
	public interface IRepository<T> : IDisposable where T : IAggregateRoot
	{
		IUnitOfWork UnitOfWork { get; }
		Task<T> ObterPorIdAsync(Guid id);
		Task<ICollection<T>> ObterTodosAsync();
		Task AdicionarAsync(T obj);
		void Atualizar(T obj);
		void Remover(T obj);
	}
}
