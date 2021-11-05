using System;
using System.Collections.Generic;

namespace NerdStore.Core.Dtos
{
	public class ListaProdutosPedido
	{
		public Guid IdPedido { get; set; }
		public ICollection<ProdutoPedido> Itens { get; set; }
	}

	public class ProdutoPedido
	{
		public Guid Id { get; set; }
		public int Quantidade { get; set; }
	}
}
