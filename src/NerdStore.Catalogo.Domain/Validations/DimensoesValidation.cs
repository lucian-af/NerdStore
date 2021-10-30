using FluentValidation;
using NerdStore.Catalogo.Domain.ValueObjects;

namespace NerdStore.Catalogo.Domain.Validations
{
	public class DimensoesValidation : AbstractValidator<Dimensoes>
	{
		public DimensoesValidation()
		{
			RuleFor(d => d.Altura)
				.GreaterThan(0)
				.WithMessage("O atributo 'Altura' deve ser maior que zero.");

			RuleFor(d => d.Largura)
				.GreaterThan(0)
				.WithMessage("O atributo 'Largura' deve ser maior que zero.");

			RuleFor(d => d.Profundidade)
				.GreaterThan(0)
				.WithMessage("O atributo 'Profundidade' deve ser maior que zero.");
		}
	}
}
