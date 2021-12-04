using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Queries.Dtos;
using NerdStore.WebApi.IntegrationTests;
using NerdStore.WebApi.Models;
using NerdStore.WebApp.Tests.Configs;
using Xunit;

namespace NerdStore.WebApp.Tests
{
	[TestCaseOrderer(nameof(PriorityOrderer), nameof(Configs))]
	[Collection(nameof(IntegrationApiTestsFixtureCollection))]
	public class PedidoApiTest
	{
		private readonly IntegrationTestsApiFixture<StartupApiTests> _testFixture;
		private readonly Produto _produto;

		public PedidoApiTest(IntegrationTestsApiFixture<StartupApiTests> testFixture)
		{
			_testFixture = testFixture;
			_produto = _testFixture.Produto ?? _testFixture.ObterProdutoAtivoComEstoque().Result;
		}

		[Fact(DisplayName = "Adicionar item acima do estoque deve retornar httpStatusCode 400 - Operação inválida"), TestPriority(1)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_AdicionarItem_EstoqueInsuficiente_DeveRetornarBadRequest()
		{
			// Arrange			
			var itemPedido = new ItemViewModel
			{
				Id = _produto.Id,
				Quantidade = 100
			};

			// Act
			var response = await _testFixture.Client.PostAsJsonAsync($"api/carrinho", itemPedido);

			// Assert
			Assert.NotNull(_produto);
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact(DisplayName = "Adicionar item em novo pedido deve retornar httpStatusCode 200 - Sucesso"), TestPriority(2)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_AdicionarItem_NovoPedido_DeveRetornarSucesso()
		{
			// Arrange			
			var itemPedido = new ItemViewModel
			{
				Id = _produto.Id,
				Quantidade = 2
			};

			// Act
			var response = await _testFixture.Client.PostAsJsonAsync("api/carrinho", itemPedido);

			// Assert
			Assert.NotNull(_produto);
			response.EnsureSuccessStatusCode();
		}

		[Fact(DisplayName = "Atualizar item do pedido deve retornar httpStatusCode 200 - Sucesso"), TestPriority(3)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_AtualizarItem_DeveRetornarSucesso()
		{
			// Arrange			
			var itemPedido = new ItemViewModel
			{
				Id = _produto.Id,
				Quantidade = 5
			};

			// Act
			var response = await _testFixture.Client.PutAsJsonAsync($"api/carrinho/{_produto.Id}", itemPedido);

			// Assert
			Assert.NotNull(_produto);
			response.EnsureSuccessStatusCode();
		}

		[Fact(DisplayName = "Adicionar item sem informar produto ou produto não encontrado retornar httpStatusCode 400 - Operação Inválida"), TestPriority(4)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_AdicionarItem_ProdutoNaoEncontrado_DeveRetornarBadRequest()
		{
			// Arrange			
			var itemPedido = new ItemViewModel
			{
				Id = Guid.NewGuid(),
				Quantidade = 2
			};

			// Act
			var response = await _testFixture.Client.PostAsJsonAsync("api/carrinho", itemPedido);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact(DisplayName = "Consultar o carrinho atual do cliente httpStatusCode 200 - Sucesso",
			  Skip = "Melhoria futura"), TestPriority(5)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_ConsultarCarrinho_DeveRetornarSucesso()
		{
			// Arrange && Act
			var response = await _testFixture.Client.GetAsync("api/carrinho");

			// Assert
			var carrinho = await response.Content.ReadFromJsonAsync<CarrinhoDto>();
			Assert.NotNull(carrinho);
			response.EnsureSuccessStatusCode();
		}

		[Fact(DisplayName = "Atualizar item para um produto não informado ou não encontrado retornar httpStatusCode 400 - Operação inválida"), TestPriority(6)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_AtualizarItem_ProdutoNaoEncontrado_DeveRetornarBadRequest()
		{
			// Arrange			
			var itemPedido = new ItemViewModel
			{
				Id = Guid.NewGuid(),
				Quantidade = 1
			};

			// Act
			var response = await _testFixture.Client.PutAsJsonAsync($"api/carrinho/{itemPedido.Id}", itemPedido);

			// Assert			
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact(DisplayName = "Remover item do carrinho deve retornar httpStatusCode 200 - Sucesso",
			  Skip = "Melhoria futura"), TestPriority(7)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_RemoverItem_DeveRetornarSucesso()
		{
			// Arrange && Act
			var response = await _testFixture.Client.DeleteAsync($"api/carrinho/{_produto.Id}");

			// Assert
			Assert.NotNull(_produto);
			response.EnsureSuccessStatusCode();
		}

		[Fact(DisplayName = "Remover item não informado ou não encontrado do carrinho deve retornar httpStatusCode 400 - Operação inválida"), TestPriority(8)]
		[Trait("Categoria", "IntegrationTests API")]
		public async Task PedidoApi_RemoverItem_ProdutoNaoEncontrado_DeveRetornarBadRequest()
		{
			// Arrange && Act
			var response = await _testFixture.Client.DeleteAsync($"api/carrinho/{Guid.NewGuid()}");

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
