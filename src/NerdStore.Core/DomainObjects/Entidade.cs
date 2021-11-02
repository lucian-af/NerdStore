using System;

namespace NerdStore.Core.DomainObjects
{
	public abstract class Entidade
	{
		public Guid Id { get; protected set; }

		protected Entidade()
			=> Id = Guid.NewGuid();

		public override bool Equals(object obj)
		{
			var compareTo = obj as Entidade;

			if (ReferenceEquals(this, compareTo))
				return true;

			if (compareTo is null)
				return false;

			return Id.Equals(compareTo.Id);
		}

		public static bool operator ==(Entidade a, Entidade b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entidade a, Entidade b)
			=> !(a == b);

		/// <summary>
		/// Método para obter o HashCode da Entidade
		/// A lógica aplicada é para garantir que seja praticamente impossível
		/// gerar Hashs iguais
		/// Dica: Número 907 é aleatório, poderia ser qualquer número maior que 1
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
			=> (GetType().GetHashCode() * 907) + Id.GetHashCode();

		public override string ToString()
			=> $"{GetType().Name} [Id={Id}]";

		public virtual void Validar()
			=> throw new NotImplementedException();
	}
}
