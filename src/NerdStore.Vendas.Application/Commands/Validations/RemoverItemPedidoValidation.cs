using System;
using FluentValidation;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Commands.Validations
{
	public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
	{
		public RemoverItemPedidoValidation()
		{
			RuleFor(c => c.IdCliente)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do cliente inválido");

			RuleFor(c => c.IdProduto)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do produto inválido");
		}
	}


}
