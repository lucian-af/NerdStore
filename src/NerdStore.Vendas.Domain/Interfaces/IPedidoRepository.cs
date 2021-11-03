using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Core.Data.Interfaces;

namespace NerdStore.Vendas.Domain.Entidades
{
	public interface IPedidoRepository : IRepository<Pedido>, IDisposable
	{
		Task<IEnumerable<Pedido>> ObterListaPorIdCliente(Guid idCliente);
		Task<Pedido> ObterPedidoRascunhoPorIdCliente(Guid idCliente);
		Task<PedidoItem> ObterItemPorId(Guid id);
		Task<PedidoItem> ObterItemPorPedido(Guid idPedido, Guid idProduto);
		void AdicionarItem(PedidoItem pedidoItem);
		void AtualizarItem(PedidoItem pedidoItem);
		void RemoverItem(PedidoItem pedidoItem);
		Task<Voucher> ObterVoucherPorCodigo(string codigo);
	}
}
