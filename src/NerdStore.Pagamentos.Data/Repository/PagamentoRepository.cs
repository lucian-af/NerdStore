using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Pagamentos.Business.Entidades;
using NerdStore.Pagamentos.Business.Interfaces;

namespace NerdStore.Pagamentos.Data.Repository
{
	public class PagamentoRepository : Repository<Pagamento, PagamentoContext>, IPagamentoRepository
	{
		private readonly PagamentoContext _context;
		private readonly DbSet<Transacao> _dbSetTransacao;

		public PagamentoRepository(PagamentoContext context)
			: base(context)
		{
			_context = context;
			_dbSetTransacao = _context.Set<Transacao>();
		}

		public void Adicionar(Pagamento pagamento)
			=> _dbSet.Add(pagamento);

		public void AdicionarTransacao(Transacao transacao)
			=> _dbSetTransacao.Add(transacao);
	}
}
