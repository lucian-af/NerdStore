using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Events
{
	public class ProdutoAbaixoEstoqueEvent : DomainEvent
	{
		public ProdutoAbaixoEstoqueEvent(Guid idAggregate, int quantidadeRestante) : base(idAggregate)
			=> QuantidadeRestante = quantidadeRestante;

		public int QuantidadeRestante { get; private set; }
	}
}
