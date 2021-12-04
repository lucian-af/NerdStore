using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using NerdStore.WebApp.Mvc.IntegrationTests;
using NerdStore.WebApp.Tests.Configs;
using Xunit;

namespace NerdStore.WebApp.Tests
{
	[Collection(nameof(IntegrationTestsFixtureCollection))]
	public class PedidoTest
	{
		private readonly IntegrationTestsFixture<StartupTests> _testFixture;

		public PedidoTest(IntegrationTestsFixture<StartupTests> testFixture) => _testFixture = testFixture;

		[Fact(DisplayName = "Adicionar item em novo pedido deve atualizar valor total")]
		[Trait("Categoria", "IntegrationTests")]
		public async Task Pedido_AdicionarItem_NovoPedido_DeveAtualizarValorTotal()
		{
			// Arrange
			var idProduto = new Guid("bd189101-cb35-4c5f-8088-29118e3e4ab3");
			var quantidade = 2;

			var initialResponse = await _testFixture.Client.GetAsync($"/produto-detalhe/{idProduto}");
			initialResponse.EnsureSuccessStatusCode();

			var formData = new Dictionary<string, string>
			{
				{_testFixture.AntiForgeryFieldName, _testFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync())},
				{"Id", idProduto.ToString()},
				{"quantidade", quantidade.ToString()}
			};

			var postRequest = new HttpRequestMessage(HttpMethod.Post, "/meu-carrinho")
			{
				Content = new FormUrlEncodedContent(formData)
			};

			// Act
			var postResponse = await _testFixture.Client.SendAsync(postRequest);

			var test = await postResponse.Content.ReadAsStringAsync();
			// Assert
			var html = new HtmlParser()
				.ParseDocumentAsync(await postResponse.Content.ReadAsStringAsync())
				.Result
				.All;

			var formQuantidade = html?.FirstOrDefault(c => c.Id == "quantidade")?.GetAttribute("value")?.NumberOnly();
			var valorUnitario = html?.FirstOrDefault(c => c.Id == "valorUnitario")?.TextContent.Split('.')[0]?.NumberOnly();
			var valorTotal = html?.FirstOrDefault(c => c.Id == "valorTotalItem")?.TextContent.Split('.')[0]?.NumberOnly();

			Assert.Equal(valorTotal, formQuantidade * valorUnitario);
		}
	}
}
