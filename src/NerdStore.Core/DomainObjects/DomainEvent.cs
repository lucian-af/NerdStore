using System;
using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
	public class DomainEvent : Event
	{
		public DomainEvent(Guid idAggregate)
			=> IdAggregate = idAggregate;
	}
}
