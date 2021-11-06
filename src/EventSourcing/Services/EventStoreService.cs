using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing.Services
{
	public class EventStoreService : IEventStoreService
	{
		private readonly IEventStoreConnection _connection;

		public EventStoreService(IConfiguration config)
		{
			_connection = EventStoreConnection.Create(config.GetConnectionString("EventStoreConnection"));
			_connection.ConnectAsync();
		}

		public IEventStoreConnection GetConnection() => _connection;
	}
}
