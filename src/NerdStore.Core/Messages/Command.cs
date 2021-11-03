using System;
using FluentValidation.Results;
using MediatR;

namespace NerdStore.Core.Messages
{
	public class Command : Message, IRequest<bool>
	{
		public DateTime Timestamp { get; private set; }
		public ValidationResult ValidationResult { get; protected set; }

		public Command()
			=> Timestamp = DateTime.Now;

		public virtual bool Valido()
			=> throw new NotImplementedException();
	}
}
