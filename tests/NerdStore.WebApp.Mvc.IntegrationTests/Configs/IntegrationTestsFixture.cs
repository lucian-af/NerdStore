using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace NerdStore.WebApp.Mvc.IntegrationTests.Configs
{
	[CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
	public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }

	public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
	{
		public string AntiForgeryFieldName = "__RequestVerificationToken";

		public readonly LojaAppFactory<TStartup> Factory;
		public HttpClient Client;

		public IntegrationTestsFixture()
		{
			var clientOptions = new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = true,
				BaseAddress = new Uri("http://localhost"),
				HandleCookies = true,
				MaxAutomaticRedirections = 7
			};

			Factory = new LojaAppFactory<TStartup>();
			Client = Factory.CreateClient(clientOptions);
		}

		public string ObterAntiForgeryToken(string htmlBody)
		{
			var requestVerificationTokenMatch =
				Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

			if (requestVerificationTokenMatch.Success)
				return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

			throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML",
				   nameof(htmlBody));
		}

		public void Dispose()
		{
			Factory.Dispose();
			Client.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
