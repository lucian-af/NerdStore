using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Vendas.Data.Context;
using NerdStore.Vendas.Domain.Entidades;
using NerdStore.Vendas.Domain.Enums;

namespace NerdStore.Vendas.Data.Repository
{
	public class PedidoRepository : Repository<Pedido, VendasContext>, IPedidoRepository
	{
		private readonly VendasContext _context;
		private readonly DbSet<PedidoItem> _dbSetPedidoItem;
		private readonly DbSet<Voucher> _dbSetVoucher;

		public PedidoRepository(VendasContext context) : base(context)
		{
			_context = context;
			_dbSetPedidoItem = context.Set<PedidoItem>();
			_dbSetVoucher = context.Set<Voucher>();
		}

		public async Task<IEnumerable<Pedido>> ObterListaPorIdCliente(Guid idCliente)
			=> await _dbSet.Where(p => p.IdCliente == idCliente).AsNoTracking().ToListAsync();

		public async Task<Pedido> ObterPedidoRascunhoPorIdCliente(Guid idCliente)
		{
			var pedido = await _dbSet.FirstOrDefaultAsync(p => p.IdCliente == idCliente &&
				p.PedidoStatus == PedidoStatus.Rascunho);
			if (pedido == null) return null;


			await _context.Entry(pedido)
				.Collection(i => i.PedidoItems).LoadAsync();

			if (pedido.IdVoucher != null)
			{
				await _context.Entry(pedido)
					.Reference(i => i.Voucher).LoadAsync();
			}

			return pedido;
		}

		public async Task<PedidoItem> ObterItemPorId(Guid id)
			=> await _dbSetPedidoItem.FindAsync(id);

		public async Task<PedidoItem> ObterItemPorPedido(Guid idPedido, Guid idProduto)
			=> await _dbSetPedidoItem.FirstOrDefaultAsync(p => p.IdProduto == idProduto && p.IdPedido == idPedido);

		public void AdicionarItem(PedidoItem pedidoItem)
			=> _dbSetPedidoItem.Add(pedidoItem);

		public void AtualizarItem(PedidoItem pedidoItem)
			=> _dbSetPedidoItem.Update(pedidoItem);

		public void RemoverItem(PedidoItem pedidoItem)
			=> _dbSetPedidoItem.Remove(pedidoItem);

		public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
			=> await _dbSetVoucher.FirstOrDefaultAsync(p => p.Codigo == codigo);
	}
}
