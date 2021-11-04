using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;

namespace NerdStore.Core.Messages
{
	public abstract class CommandHandler
	{
		protected readonly IMediatorHandler _mediatorHandler;
		protected CommandHandler(IMediatorHandler mediatorHandler)
			=> _mediatorHandler = mediatorHandler;

		/// <summary>
		/// Validar command com disparo de eventos
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		protected bool ValidarComando(Command message)
			=> message.Valido() || NotificarErros(message);

		protected bool NotificarErros(Command message)
		{
			message.ValidationResult.Errors
				.ForEach(erro => _mediatorHandler
				.PublicarNotificacao(new DomainNotification(message.MessageType, erro.ErrorMessage)));

			return false;
		}
	}
}
