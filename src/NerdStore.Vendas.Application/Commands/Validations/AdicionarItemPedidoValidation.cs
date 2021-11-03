﻿using System;
using FluentValidation;
using NerdStore.Vendas.Application.Commands.Models;

namespace NerdStore.Vendas.Application.Commands.Validations
{
	public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
	{
		public AdicionarItemPedidoValidation()
		{
			RuleFor(c => c.IdCliente)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do cliente inválido.");

			RuleFor(c => c.IdProduto)
				.NotEqual(Guid.Empty)
				.WithMessage("Id do produto inválido.");

			RuleFor(c => c.Nome)
				.NotEmpty()
				.WithMessage("O nome do produto não foi informado.");

			RuleFor(c => c.Quantidade)
				.GreaterThan(0)
				.WithMessage("A quantidade miníma de um item é um.");

			RuleFor(c => c.Quantidade)
				.LessThan(15)
				.WithMessage("A quantidade máxima de um item é 15.");

			RuleFor(c => c.ValorUnitario)
				.GreaterThan(0)
				.WithMessage("O valor do item precisa ser maior que zero.");
		}

	}
}
