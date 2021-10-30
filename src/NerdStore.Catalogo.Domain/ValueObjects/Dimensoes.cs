using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Catalogo.Domain.Validations;
using NerdStore.Core.Exceptions;

namespace NerdStore.Catalogo.Domain.ValueObjects
{
	public sealed class Dimensoes : IValueObject
	{
		public Dimensoes(
			decimal altura,
			decimal largura,
			decimal profundidade)
		{
			Altura = altura;
			Largura = largura;
			Profundidade = profundidade;

			Validar();
		}

		public decimal Altura { get; private set; }
		public decimal Largura { get; private set; }
		public decimal Profundidade { get; private set; }

		public void Validar()
		{
			var result = new DimensoesValidation().Validate(this);

			if (!result.IsValid)
				throw new DomainException(string.Join('|', result.Errors));
		}

		public string DescricaoFormatada()
			=> $"LxAxP: {Largura} x {Altura} x {Profundidade}";

		public override string ToString()
			=> DescricaoFormatada();
	}
}
