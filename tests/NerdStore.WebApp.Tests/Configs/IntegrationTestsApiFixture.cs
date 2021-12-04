using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.WebApi.IntegrationTests;
using NerdStore.WebApi.Models;
using Xunit;

namespace NerdStore.WebApp.Tests.Configs
{
	[CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
	public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsApiFixture<StartupApiTests>> { }

	public class IntegrationTestsApiFixture<TStartup> : IDisposable where TStartup : class
	{
		public string Token;

		public readonly LojaAppFactory<TStartup> Factory;
		public readonly IServiceScope Scope;
		public HttpClient Client;
		public Faker Faker;
		public Produto Produto;

		public IntegrationTestsApiFixture()
		{
			Factory = new LojaAppFactory<TStartup>();
			Client = Factory.CreateClient();
			Scope = Factory.Services.CreateScope();
			Faker = new Faker("pt_BR");
			Autenticar().GetAwaiter().GetResult();
		}

		public async Task Autenticar()
		{
			if (!string.IsNullOrWhiteSpace(Token))
				return;

			var usuario = new LoginViewModel
			{
				Email = Faker.Person.Email,
				Senha = Faker.Random.AlphaNumeric(8)
			};

			var postResponse = await Client.PostAsJsonAsync("api/login", usuario);
			postResponse.EnsureSuccessStatusCode();
			Token = await postResponse.Content.ReadAsStringAsync();

			Client.AddToken(Token);
		}

		public async Task<Produto> ObterProdutoAtivoComEstoque()
		{
			if (Produto != null)
				return Produto;

			var _produtoRepo = Scope.ServiceProvider.GetRequiredService<IProdutoRepository>();
			var categorias = await _produtoRepo.ObterCategorias();
			Produto = (await _produtoRepo.ObterPorCategoria(categorias.FirstOrDefault().Codigo))
				.FirstOrDefault(p => p.Ativo && p.QuantidadeEstoque > 0);

			return Produto;
		}

		public void Dispose()
		{
			Factory.Dispose();
			Client.Dispose();
			Scope.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
