using System;
using NerdStore.Core.DomainObjects;
using NerdStore.Pagamentos.Business.Models.Enums;

namespace NerdStore.Pagamentos.Business.Entidades
{
	public class Transacao : Entidade
	{
		public Guid IdPedido { get; set; }
		public Guid IdPagamento { get; set; }
		public decimal Total { get; set; }
		public StatusTransacao StatusTransacao { get; set; }

		// EF. Rel.
		public Pagamento Pagamento { get; set; }
	}
}
