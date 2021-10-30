using System;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.Exceptions;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests.Entities
{
	public class ProdutoTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Produto_Validar_Nome_NuloVazioOuEspacoEmBranco_DeveRetornarException(string nome)
		{
			// Assert && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Produto(
					nome,
					"TESTE",
					true,
					1.50m,
					Guid.NewGuid(),
					DateTime.Now,
					"C:/tmp",
					new Dimensoes(1, 1, 1)
					));

			Assert.Equal("O atributo 'Nome' do produto é obrigatório.", act.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Produto_Validar_Descricao_NuloVazioOuEspacoEmBranco_DeveRetornarException(string descricao)
		{
			// Assert && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Produto(
					"TESTE PRODUTO",
					descricao,
					true,
					1.50m,
					Guid.NewGuid(),
					DateTime.Now,
					"C:/tmp",
					new Dimensoes(1, 1, 1)
					));

			Assert.Equal("O atributo 'Descricao' do produto é obrigatório.", act.Message);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Produto_Validar_Valor_ZeroOuNegativo_DeveRetornarException(decimal valor)
		{
			// Assert && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Produto(
					"TESTE PRODUTO",
					"TESTE",
					true,
					valor,
					Guid.NewGuid(),
					DateTime.Now,
					"C:/tmp",
					new Dimensoes(1, 1, 1)
					));

			Assert.Equal("O valor do produto deve ser maior que zero.", act.Message);
		}

		[Fact]
		public void Produto_Validar_IdCategoria_Nulo_DeveRetornarException()
		{
			// Assert && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Produto(
					"TESTE PRODUTO",
					"TESTE",
					true,
					1.50m,
					Guid.Empty,
					DateTime.Now,
					"C:/tmp",
					new Dimensoes(1, 1, 1)
					));

			Assert.Equal("O atributo 'IdCategoria' do produto é obrigatório.", act.Message);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Produto_Validar_Imagem_NuloVazioOuEspacoEmBranco_DeveRetornarException(string imagem)
		{
			// Assert && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Produto(
					"TESTE PRODUTO",
					"TESTE",
					true,
					1.50m,
					Guid.NewGuid(),
					DateTime.Now,
					imagem,
					new Dimensoes(1, 1, 1)
					));

			Assert.Equal("O atributo 'Imagem' do produto é obrigatório.", act.Message);
		}

		[Fact]
		public void Produto_Validar_NaoDeveRetornarException()
		{
			// Assert && Act && Assert
			var act = new Produto(
				"TESTE PRODUTO",
				"TESTE",
				true,
				1.50m,
				Guid.NewGuid(),
				DateTime.Now,
				"c:/tmp",
				  new Dimensoes(1, 1, 1)
				  );

			Assert.NotNull(act);
		}
	}
}
