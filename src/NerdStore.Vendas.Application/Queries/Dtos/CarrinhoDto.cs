using System;
using System.Collections.Generic;

namespace NerdStore.Vendas.Application.Queries.Dtos
{
	public class CarrinhoDto
	{
		public Guid IdPedido { get; set; }
		public Guid IdCliente { get; set; }
		public decimal SubTotal { get; set; }
		public decimal ValorTotal { get; set; }
		public decimal ValorDesconto { get; set; }
		public string VoucherCodigo { get; set; }

		public List<CarrinhoItemDto> Items { get; set; } = new List<CarrinhoItemDto>();
		public CarrinhoPagamentoDto Pagamento { get; set; }
	}
}
