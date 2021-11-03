using System;

namespace NerdStore.Core.Messages.Common.DomainEvents
{
	public class DomainEvent : Event
	{
		public DomainEvent(Guid idAggregate)
			=> IdAggregate = idAggregate;
	}
}
