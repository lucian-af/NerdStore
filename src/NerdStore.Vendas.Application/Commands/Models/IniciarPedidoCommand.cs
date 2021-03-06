using System;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Commands.Validations;

namespace NerdStore.Vendas.Application.Commands.Models
{
	public class IniciarPedidoCommand : Command
	{
		public Guid IdPedido { get; private set; }
		public Guid IdCliente { get; private set; }
		public decimal Total { get; private set; }
		public string NomeCartao { get; private set; }
		public string NumeroCartao { get; private set; }
		public string ExpiracaoCartao { get; private set; }
		public string CvvCartao { get; private set; }

		public IniciarPedidoCommand(
			Guid idPedido,
			Guid idCliente,
			decimal total,
			string nomeCartao,
			string numeroCartao,
			string expiracaoCartao,
			string cvvCartao)
		{
			IdPedido = idPedido;
			IdCliente = idCliente;
			Total = total;
			NomeCartao = nomeCartao;
			NumeroCartao = numeroCartao;
			ExpiracaoCartao = expiracaoCartao;
			CvvCartao = cvvCartao;
		}

		public override bool Valido()
		{
			ValidationResult = new IniciarPedidoValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}
