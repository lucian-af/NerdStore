using System;
using FluentValidation;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Domain.Validations
{
	public class ProdutoValidation : AbstractValidator<Produto>
	{
		public ProdutoValidation()
		{
			RuleFor(p => p.Nome)
				.NotEmpty()
				.WithMessage("O Nome do produto é obrigatório.");

			RuleFor(p => p.Descricao)
				.NotEmpty()
				.WithMessage("A Descrição do produto é obrigatório.");

			RuleFor(p => p.IdCategoria)
				.NotEqual(Guid.Empty)
				.WithMessage("O Id da Categoria do produto é obrigatório.");

			RuleFor(p => p.Valor)
				.GreaterThanOrEqualTo(0.01m)
				.WithMessage("O valor do produto deve ser maior que zero.");
		}
	}
}
