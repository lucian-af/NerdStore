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
				.WithMessage("O Nome da categoria é obrigatório.");

			RuleFor(ca => ca.Codigo)
				.NotEmpty()
				.WithMessage("O Código da categoria é obrigatório.");
		}
	}
}
