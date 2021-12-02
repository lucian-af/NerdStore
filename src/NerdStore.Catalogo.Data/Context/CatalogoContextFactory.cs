using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NerdStore.Catalogo.Data.Context
{
	public class CatalogoContextFactory : IDesignTimeDbContextFactory<CatalogoContext>
	{
		public CatalogoContext CreateDbContext(string[] args)
		{

			IConfiguration config = new ConfigurationBuilder()
							.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NerdStore.WebApp.Mvc"))
							.AddJsonFile("appsettings.json")
							.AddJsonFile($"appsettings.Tests.json", true, true)
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
