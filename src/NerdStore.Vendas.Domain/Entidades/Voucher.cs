using System;
using System.Collections.Generic;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Validations;

namespace NerdStore.Vendas.Domain.Entidades
{
	public class Voucher : Entidade
	{
		public Voucher(
			string codigo,
			decimal? percentual,
			decimal? valorDesconto,
			int quantidade,
			TipoDescontoVoucher tipoDescontoVoucher,
			DateTime dataValidade)
		{
			Codigo = codigo;
			Percentual = percentual;
			ValorDesconto = valorDesconto;
			Quantidade = quantidade;
			TipoDescontoVoucher = tipoDescontoVoucher;
			DataCriacao = DateTime.Now;
			DataValidade = dataValidade;
			Ativo = quantidade > 0;
		}

		public string Codigo { get; private set; }
		public decimal? Percentual { get; private set; }
		public decimal? ValorDesconto { get; private set; }
		public int Quantidade { get; private set; }
		public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
		public DateTime DataCriacao { get; private set; }
		public DateTime? DataUtilizacao { get; private set; }
		public DateTime DataValidade { get; private set; }
		public bool Ativo { get; private set; }
		public bool Utilizado { get; private set; }

		public virtual ICollection<Pedido> Pedidos { get; set; }

		internal ValidationResult ValidarSeAplicavel()
			=> new VoucherValidation().Validate(this);

		internal void DebitarQuantidadeVoucher()
		{
			if (Quantidade == 0)
			{
				return;
			}

			Quantidade -= 1;
			DataUtilizacao = DateTime.Now;

			if (Quantidade == 0)
			{
				Utilizado = true;
				Ativo = false;
			}
		}
	}
}
