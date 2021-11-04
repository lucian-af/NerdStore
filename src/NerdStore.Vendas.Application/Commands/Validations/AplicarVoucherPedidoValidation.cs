using System;
using FluentValidation;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Commands.Validations
{
	public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
	{
		public AplicarVoucherPedidoValidation()
		{
			RuleFor(c => c.IdCliente)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do cliente inválido");

			RuleFor(c => c.CodigoVoucher)
				.NotEmpty()
				.WithMessage("O código do voucher não pode ser vazio");
		}
	}
}
