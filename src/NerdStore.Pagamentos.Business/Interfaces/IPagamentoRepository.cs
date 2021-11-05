using NerdStore.Core.Data.Interfaces;
using NerdStore.Pagamentos.Business.Entidades;

namespace NerdStore.Pagamentos.Business.Interfaces
{
	public interface IPagamentoRepository : IRepository<Pagamento>
	{
		void Adicionar(Pagamento pagamento);

		void AdicionarTransacao(Transacao transacao);
	}
}
