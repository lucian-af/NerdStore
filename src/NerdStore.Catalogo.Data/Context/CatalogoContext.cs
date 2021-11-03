using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.Messages;

namespace NerdStore.Catalogo.Data.Context
{
	public class CatalogoContext : DbContext, IUnitOfWork
	{
		public CatalogoContext(DbContextOptions<CatalogoContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Todas as propriedades que forem do tipo string e não tiverem um mapeamento específico
			// será criada no banco como varchar(100)
			modelBuilder.Model
				.GetEntityTypes()
				.SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))
				.ToList()
				.ForEach(prop => prop.SetColumnType("varchar(100)"));

			modelBuilder.Ignore<Event>();

			// Busca os mapeamentos criados via reflection uma única vez para criação no banco
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
		}

		public async Task<bool> Commit()
		{
			// Todas as entidades que tenham o campo DataCadastro
			// se o estado for de insert vai definir a data/hora como Now;
			// se o estado for alteração vai resetar o estado da propriedade para não modificado para que
			//   o valor do momento da criação do registro seja mantido.
			foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property("DataCadastro").CurrentValue = DateTime.Now;
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Property("DataCadastro").IsModified = false;
				}
			}

			return await base.SaveChangesAsync() > 0;
		}
	}
}
