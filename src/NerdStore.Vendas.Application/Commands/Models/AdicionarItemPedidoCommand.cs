using System;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validations;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class AdicionarItemPedidoCommand : Command
	{
		public Guid IdCliente { get; private set; }
		public Guid IdProduto { get; private set; }
		public string Nome { get; private set; }
		public int Quantidade { get; private set; }
		public decimal ValorUnitario { get; private set; }

		public AdicionarItemPedidoCommand(
			Guid idCliente,
			Guid idProduto,
			string nome,
			int quantidade,
			decimal valorUnitario)
		{
			IdCliente = idCliente;
			IdProduto = idProduto;
			Nome = nome;
			Quantidade = quantidade;
			ValorUnitario = valorUnitario;
		}

		public override bool Valido()
		{
			ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
