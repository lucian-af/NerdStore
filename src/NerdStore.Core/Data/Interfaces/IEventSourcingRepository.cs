using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Data.Interfaces
{
	public interface IEventSourcingRepository
	{
		Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
		Task<IEnumerable<StoredEvent>> ObterEventos(Guid idAggregate);
	}
}
