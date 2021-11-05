using NerdStore.Pagamentos.Business.Dtos;
using NerdStore.Pagamentos.Business.Entidades;

namespace NerdStore.Pagamentos.Business.Interfaces
{
	public interface IPagamentoCartaoCreditoFacade
	{
		Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
	}
}
