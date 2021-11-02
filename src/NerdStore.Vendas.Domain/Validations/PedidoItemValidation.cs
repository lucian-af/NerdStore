using System;
using FluentValidation;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Domain.Validations
{
	public class PedidoItemValidation : AbstractValidator<PedidoItem>
	{
		public PedidoItemValidation()
		{
			RuleFor(p => p.IdProduto)
				.NotEqual(Guid.Empty)
				.WithMessage("O atributo 'IdProduto' é obrigatório.");

			RuleFor(p => p.NomeProduto)
				.NotEmpty()
				.WithMessage("O atributo 'NomeProduto' é obrigatório.");

			RuleFor(p => p.Quantidade)
				.GreaterThanOrEqualTo(1)
				.WithMessage("O atributo 'Quantidade' deve ser maior ou igual a um.");

			RuleFor(p => p.ValorUnitario)
				.GreaterThanOrEqualTo(0.01m)
				.WithMessage("O atributo 'ValorUnitario' deve ser maior que zero.");
		}
	}
}
