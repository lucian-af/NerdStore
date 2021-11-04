using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Queries.Dtos.Interfaces
{
	public interface IPedidoQueries
	{
		Task<CarrinhoDto> ObterCarrinhoCliente(Guid idCliente);
		Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid idCliente);
	}
}
