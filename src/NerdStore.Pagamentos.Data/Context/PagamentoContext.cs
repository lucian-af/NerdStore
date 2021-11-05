using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;

namespace NerdStore.Pagamentos.Data
{
	public class PagamentoContext : DbContext, IUnitOfWork
	{
		private readonly IMediatorHandler _mediatorHandler;

		public PagamentoContext(DbContextOptions<PagamentoContext> options, IMediatorHandler rebusHandler)
			: base(options)
			=> _mediatorHandler = rebusHandler;


		public async Task<bool> Commit()
		{
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

			var sucesso = await base.SaveChangesAsync() > 0;

			if (sucesso)
				await _mediatorHandler.PublicarEventos(this);

			return sucesso;
		}

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

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);

			modelBuilder.Model.GetEntityTypes()
				.SelectMany(e => e.GetForeignKeys())
				.ToList()
				.ForEach(relationship => relationship.DeleteBehavior = DeleteBehavior.ClientSetNull);

			base.OnModelCreating(modelBuilder);
		}
	}
}
