using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class PedidoProdutoAtualizadoEvent : Event
	{
		public Guid IdCliente { get; private set; }
		public Guid IdPedido { get; private set; }
		public Guid IdProduto { get; private set; }
		public int Quantidade { get; private set; }

		public PedidoProdutoAtualizadoEvent(Guid idCliente, Guid idPedido, Guid idProduto, int quantidade)
		{
			IdAggregate = idPedido;
			IdCliente = idCliente;
			IdPedido = idPedido;
			IdProduto = idProduto;
			Quantidade = quantidade;
		}
	}
}
