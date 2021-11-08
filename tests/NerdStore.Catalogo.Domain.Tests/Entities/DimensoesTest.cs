using NerdStore.Catalogo.Domain.ValueObjects;
using NerdStore.Core.Exceptions;
using Xunit;

namespace NerdStore.Catalogo.Domain.Tests.Entities
{
	public class DimensoesTest
	{
		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Dimensoes_Validar_Altura_MenorOuIgualZero_DeveRetornarException(decimal altura)
		{
			// Arrange && Act
			var act = Assert.Throws<DomainException>(() =>
					new Dimensoes(altura, 1, 1));

			// Assert
			Assert.Equal("O atributo 'Altura' deve ser maior que zero.", act.Message);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Dimensoes_Validar_Largura_MenorOuIgualZero_DeveRetornarException(decimal largura)
		{
			// Arrange && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
					new Dimensoes(1, largura, 1));

			Assert.Equal("O atributo 'Largura' deve ser maior que zero.", act.Message);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Dimensoes_Validar_Profundidade_MenorOuIgualZero_DeveRetornarException(decimal profundidade)
		{
			// Arrange && Act && Assert
			var act = Assert.Throws<DomainException>(() =>
					new Dimensoes(1, 1, profundidade)
					);

			Assert.Equal("O atributo 'Profundidade' deve ser maior que zero.", act.Message);
		}

		[Fact]
		public void Dimensoes_Validar_NaoDeveRetornarException()
		{
			// Arrange && Act && Assert
			var act = new Dimensoes(1, 1, 1);

			Assert.NotNull(act);
		}
	}
}
