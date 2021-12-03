using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;

namespace NerdStore.WebApi.Controllers
{
	public abstract class ControllerBase : Controller
	{
		private readonly DomainNotificationHandler _notifications;
		private readonly IMediatorHandler _mediatorHandler;

		protected Guid IdCliente = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

		protected ControllerBase(INotificationHandler<DomainNotification> notifications,
								 IMediatorHandler mediatorHandler)
		{
			_notifications = (DomainNotificationHandler)notifications;
			_mediatorHandler = mediatorHandler;
		}

		protected bool OperacaoValida()
			=> !_notifications.PossuiNotificacoes();

		protected IEnumerable<string> ObterMensagensErro()
			=> _notifications.ObterNotificacoes().Select(c => c.Value).ToList();

		protected void NotificarErro(string codigo, string mensagem)
			=> _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));

		protected new IActionResult Response(object result = null)
		{
			if (OperacaoValida())
			{
				return Ok(new
				{
					success = true,
					data = result
				});
			}

			return BadRequest(new
			{
				success = false,
				errors = _notifications.ObterNotificacoes().Select(n => n.Value)
			});
		}
	}
}
