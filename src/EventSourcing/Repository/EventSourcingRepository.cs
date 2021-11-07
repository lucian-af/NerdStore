using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing.Repository
{
	public class EventSourcingRepository : IEventSourcingRepository
	{
		private readonly IEventStoreService _eventStoreService;

		public EventSourcingRepository(IEventStoreService eventStoreService)
			=> _eventStoreService = eventStoreService;

		public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid idAggregate)
		{
			var eventos = await _eventStoreService
				.GetConnection()
				.ReadStreamEventsBackwardAsync(idAggregate.ToString(), 0, 500, false);

			var listaEventos = new List<StoredEvent>();

			eventos.Events.ForEach(resolvedEvent =>
			{
				var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
				var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

				var evento = new StoredEvent(
					resolvedEvent.Event.EventId,
					resolvedEvent.Event.EventType,
					jsonData.Timestamp,
					dataEncoded);

				listaEventos.Add(evento);
			});

			return listaEventos;
		}

		public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
			=> await _eventStoreService
				.GetConnection()
				.AppendToStreamAsync(
				evento.IdAggregate.ToString(),
				ExpectedVersion.Any,
				FormatarEvento(evento));

		private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
		{
			yield return new EventData(
				eventId: Guid.NewGuid(),
				type: evento.MessageType,
				isJson: true,
				data: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evento)),
				 metadata: null);
		}

		internal class BaseEvent
		{
			public DateTime Timestamp { get; set; }
		}

		/* Não implementamos IDisposable no repository para trabalhar com o Event Store
		 * a própria documentação recomenda não fechar a conexão, pois o evento é monitorado constantemente
		 * enquanto a aplicação está de pé
		*/
	}
}
