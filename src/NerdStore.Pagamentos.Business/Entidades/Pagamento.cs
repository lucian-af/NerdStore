using System;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Interfaces;

namespace NerdStore.Pagamentos.Business.Entidades
{
	public class Pagamento : Entidade, IAggregateRoot
	{
		public Guid IdPedido { get; set; }
		public string Status { get; set; }
		public decimal Valor { get; set; }

		public string NomeCartao { get; set; }
		public string NumeroCartao { get; set; }
		public string ExpiracaoCartao { get; set; }
		public string CvvCartao { get; set; }

		// EF. Rel.
		public Transacao Transacao { get; set; }
	}
}
