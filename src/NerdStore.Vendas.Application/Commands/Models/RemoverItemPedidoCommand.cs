using System;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validations;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class RemoverItemPedidoCommand : Command
	{
		public Guid IdCliente { get; private set; }
		public Guid IdProduto { get; private set; }

		public RemoverItemPedidoCommand(Guid idCliente, Guid idProduto)
		{
			IdCliente = idCliente;
			IdProduto = idProduto;
		}

		public override bool Valido()
		{
			ValidationResult = new RemoverItemPedidoValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}


}
