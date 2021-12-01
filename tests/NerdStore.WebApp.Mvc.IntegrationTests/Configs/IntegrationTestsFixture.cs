using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NerdStore.WebApp.Mvc.IntegrationTests.Configs
{
	[CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
	public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }

	public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
	{
		public readonly LojaAppFactory<TStartup> Factory;
		public HttpClient Client;

		public IntegrationTestsFixture()
		{
			var clientOptions = new WebApplicationFactoryClientOptions { };

			Factory = new LojaAppFactory<TStartup>();
			Client = Factory.CreateClient(clientOptions);
		}

		public void Dispose()
		{
			Factory.Dispose();
			Client.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
