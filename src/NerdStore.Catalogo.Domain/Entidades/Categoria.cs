using System.Collections.Generic;
using NerdStore.Catalogo.Domain.Validations;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Exceptions;

namespace NerdStore.Catalogo.Domain.Entidades
{
	public class Categoria : Entidade
	{
		public Categoria(string nome, int codigo)
		{
			Nome = nome;
			Codigo = codigo;

			Validar();
		}

		public string Nome { get; private set; }
		public int Codigo { get; private set; }

		public virtual ICollection<Produto> Produtos { get; set; }

		public override string ToString()
			=> $"{Codigo} - {Nome}";
		public override void Validar()
		{
			var result = new CategoriaValidation().Validate(this);

			if (!result.IsValid)
				throw new DomainException(string.Join('|', result.Errors));
		}
	}
}
