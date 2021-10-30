using System.Threading.Tasks;

namespace NerdStore.Core.Data.Interfaces
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}
