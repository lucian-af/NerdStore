using System;
using NerdStore.Catalogo.Domain.Validations;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;
using NerdStore.Core.Interfaces;

namespace NerdStore.Catalogo.Domain.Entidades
{
	public class Produto : Entidade, IAggregateRoot
	{
		public Guid IdCategoria { get; private set; }
		public string Nome { get; private set; }
		public string Descricao { get; private set; }
		public bool Ativo { get; private set; }
		public decimal Valor { get; private set; }
		public DateTime DataCadastro { get; private set; }
		public string Imagem { get; private set; }
		public int QuantidadeEstoque { get; private set; }
		public Categoria Categoria { get; private set; }

		public Produto(
			string nome,
			string descricao,
			bool ativo,
			decimal valor,
			Guid idCategoria,
			DateTime dataCadastro,
			string imagem)
		{
			Nome = nome;
			Descricao = descricao;
			Ativo = ativo;
			Valor = valor;
			IdCategoria = idCategoria;
			DataCadastro = dataCadastro;
			Imagem = imagem;

			Validar();
		}

		public void Ativar() => Ativo = true;
		public void Desativar() => Ativo = false;
		public void AlterarCategoria(Categoria categoria)
		{
			Categoria = categoria;
			IdCategoria = categoria.Id;
		}
		public void AlterarDescricao(string descricao)
			=> Descricao = descricao;
		public void RetirarEstoque(int quantidade)
		{
			if (quantidade < 0)
				quantidade *= -1;

			QuantidadeEstoque -= quantidade;
		}
		public void AcrescentarEstoque(int quantidade)
		{
			if (quantidade > 0)
				QuantidadeEstoque += quantidade;
		}
		public bool ExisteEstoque(int quantidade)
			=> QuantidadeEstoque >= quantidade;

		public override void Validar()
		{
			var result = new ProdutoValidation().Validate(this);

			if (!result.IsValid)
				throw new DomainException(string.Join('|', result.Errors));
		}
	}
}
