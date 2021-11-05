using System.Threading.Tasks;
using NerdStore.Core.Dtos;
using NerdStore.Pagamentos.Business.Entidades;

namespace NerdStore.Pagamentos.Business.Interfaces
{
	public interface IPagamentoService
	{
		Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
	}
}
