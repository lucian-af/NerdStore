using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events.Models
{
	public class VoucherAplicadoPedidoEvent : Event
	{
		public Guid IdCliente { get; private set; }
		public Guid IdPedido { get; private set; }
		public Guid IdVoucher { get; private set; }

		public VoucherAplicadoPedidoEvent(Guid idCliente, Guid idPedido, Guid idVoucher)
		{
			IdAggregate = idPedido;
			IdCliente = idCliente;
			IdPedido = idPedido;
			IdVoucher = idVoucher;
		}
	}
}
