using System;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validations;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class AtualizarItemPedidoCommand : Command
	{
		public Guid IdCliente { get; private set; }
		public Guid IdProduto { get; private set; }
		public int Quantidade { get; private set; }

		public AtualizarItemPedidoCommand(Guid idCliente, Guid idProduto, int quantidade)
		{
			IdCliente = idCliente;
			IdProduto = idProduto;
			Quantidade = quantidade;
		}

		public override bool Valido()
		{
			ValidationResult = new AtualizarItemPedidoValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
