using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NerdStore.Catalogo.Data.Context;

namespace NerdStore.WebApp.Mvc.Data
{
	public class CatalogoContextFactory : IDesignTimeDbContextFactory<CatalogoContext>
	{
		public CatalogoContext CreateDbContext(string[] args)
		{

			IConfiguration config = new ConfigurationBuilder()
							.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NerdStore.Catalogo.Data"))
							.AddJsonFile("appsettings.json")
							.Build();

			var connectionString = config.GetConnectionString("DefaultConnection");

			if (string.IsNullOrEmpty(connectionString))
				throw new DbUpdateException("String de conexão 'DefaultConnection' não está configurada.");

			var optionsBuilder = new DbContextOptionsBuilder<CatalogoContext>();
			optionsBuilder.UseSqlServer(connectionString,
				  c => c.MigrationsAssembly("NerdStore.Catalogo.Data"));

			optionsBuilder.UseSqlServer(connectionString);

			return new CatalogoContext(optionsBuilder.Options);
		}
	}
}
