using System;
using MediatR;

namespace NerdStore.Core.Messages.Common.DomainEvents
{
	public abstract class DomainEvent : Message, INotification
	{
		public DateTime Timestamp { get; private set; }

		protected DomainEvent(Guid idAggregate)
		{
			IdAggregate = idAggregate;
			Timestamp = DateTime.Now;
		}
	}
}
