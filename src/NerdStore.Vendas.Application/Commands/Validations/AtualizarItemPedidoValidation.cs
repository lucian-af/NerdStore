using System;
using FluentValidation;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Commands.Validations
{
	public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
	{
		public AtualizarItemPedidoValidation()
		{
			RuleFor(c => c.IdCliente)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do cliente inválido");

			RuleFor(c => c.IdProduto)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do produto inválido");

			RuleFor(c => c.Quantidade)
				.GreaterThan(0)
				.WithMessage("A quantidade miníma de um item é 1");

			RuleFor(c => c.Quantidade)
				.LessThan(15)
				.WithMessage("A quantidade máxima de um item é 15");
		}
	}
}
