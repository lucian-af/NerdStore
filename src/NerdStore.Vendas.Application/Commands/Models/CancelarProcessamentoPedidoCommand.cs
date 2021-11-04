using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class CancelarProcessamentoPedidoCommand : Command
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }

		public CancelarProcessamentoPedidoCommand(Guid idPedido, Guid idCliente)
		{
			IdAggregate = idPedido;
			IdPedido = idPedido;
			IdCliente = idCliente;
		}
	}
}
