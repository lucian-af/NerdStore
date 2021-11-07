using System.Threading.Tasks;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Common.DomainEvents;
using NerdStore.Core.Messages.Common.Notifications;

namespace NerdStore.Core.Communication.Interfaces
{
	public interface IMediatorHandler
	{
		Task PublicarEvento<T>(T evento) where T : Event;
		Task<bool> EnviarComando<T>(T comando) where T : Command;
		Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
		Task PublicarEventoDominio<T>(T evento) where T : DomainEvent;
	}
}
