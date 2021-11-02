using System;
using FluentValidation;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Domain.Validations
{
	public class VoucherValidation : AbstractValidator<Voucher>
	{
		public VoucherValidation()
		{
			RuleFor(v => v.DataValidade)
				.Must(data => data > DateTime.Now)
				.WithMessage("Voucher expirado.");

			RuleFor(v => v.Ativo)
				.Equal(true)
				.WithMessage("Voucher inválido.");

			RuleFor(v => v.Utilizado)
				.Equal(false)
				.WithMessage("Voucher utilizado.");

			RuleFor(v => v.Quantidade)
				.GreaterThan(0)
				.WithMessage("Voucher indisponível.");
		}
	}
}
