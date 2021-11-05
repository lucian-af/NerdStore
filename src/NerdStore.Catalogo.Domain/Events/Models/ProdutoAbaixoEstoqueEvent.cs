using System;
using NerdStore.Core.Messages.Common.DomainEvents;

namespace NerdStore.Catalogo.Domain.Events.Models
{
	public class ProdutoAbaixoEstoqueEvent : DomainEvent
	{
		public ProdutoAbaixoEstoqueEvent(Guid idAggregate, int quantidadeRestante) : base(idAggregate)
			=> QuantidadeRestante = quantidadeRestante;

		public int QuantidadeRestante { get; private set; }
	}
}
