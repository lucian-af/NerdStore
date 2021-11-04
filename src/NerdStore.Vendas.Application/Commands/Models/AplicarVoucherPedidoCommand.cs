using System;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validations;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class AplicarVoucherPedidoCommand : Command
	{
		public Guid IdCliente { get; private set; }
		public string CodigoVoucher { get; private set; }

		public AplicarVoucherPedidoCommand(Guid idCliente, string codigoVoucher)
		{
			IdCliente = idCliente;
			CodigoVoucher = codigoVoucher;
		}

		public override bool Valido()
		{
			ValidationResult = new AplicarVoucherPedidoValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
