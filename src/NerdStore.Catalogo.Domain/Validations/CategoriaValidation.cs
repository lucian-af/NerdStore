using FluentValidation;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Domain.Validations
{
	public class CategoriaValidation : AbstractValidator<Categoria>
	{
		public CategoriaValidation()
		{
			RuleFor(ca => ca.Nome)
				.NotEmpty()
				.WithMessage("O atributo 'Nome' da categoria é obrigatório.");

			RuleFor(ca => ca.Codigo)
				.GreaterThan(0)
				.WithMessage("O atributo 'Codigo' da categoria deve ser um número maior que zero.");
		}
	}
}
