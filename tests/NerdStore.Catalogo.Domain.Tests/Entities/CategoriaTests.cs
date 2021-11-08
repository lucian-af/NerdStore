using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Core.Exceptions;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests.Entities
{
	public class CategoriaTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Categoria_Validar_Nome_NuloVazioOuEspacoEmBranco_DeveRetornarException(string nome)
		{
			// Arrange && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Categoria(nome, 1));

			Assert.Equal("O atributo 'Nome' da categoria é obrigatório.", act.Message);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Categoria_Validar_Codigo_MenorOuigualZero_DeveRetornarException(int codigo)
		{
			// Arrange && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
				new Categoria("TESTE", codigo));

			Assert.Equal("O atributo 'Codigo' da categoria deve ser um número maior que zero.", act.Message);
		}

		[Fact]
		public void Categoria_Validar_NaoDeveRetornarException()
		{
			// Arrange && Act && Assert
			var act = new Categoria("TESTE", 100);

			Assert.NotNull(act);
		}
	}
}
