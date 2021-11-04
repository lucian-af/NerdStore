using System;

namespace NerdStore.Vendas.Application.Queries.Dtos
{
	public class CarrinhoItemDto
	{
		public Guid IdProduto { get; set; }
		public string NomeProduto { get; set; }
		public int Quantidade { get; set; }
		public decimal ValorUnitario { get; set; }
		public decimal ValorTotal { get; set; }

	}
}
