using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Vendas.Data.Context;

namespace NerdStore.Catalogo.Data.Context
{
	public class VendaContextFactory : IDesignTimeDbContextFactory<VendasContext>
	{
		public VendasContext CreateDbContext(string[] args)
		{
			IMediatorHandler _mediatorHandler = null;

			IConfiguration config = new ConfigurationBuilder()
								.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NerdStore.WebApp.Mvc"))
								.AddJsonFile("appsettings.json")
								.AddJsonFile($"appsettings.Tests.json", true, true)
								.Build();

			var connectionString = config.GetConnectionString("DefaultConnection");

			if (string.IsNullOrEmpty(connectionString))
				throw new DbUpdateException("String de conexão 'DefaultConnection' não está configurada.");

			var optionsBuilder = new DbContextOptionsBuilder<VendasContext>();
			optionsBuilder.UseSqlServer(connectionString,
				  c => c.MigrationsAssembly("NerdStore.Vendas.Data"));

			optionsBuilder.UseSqlServer(connectionString);

			return new VendasContext(optionsBuilder.Options, _mediatorHandler);
		}
	}
}
