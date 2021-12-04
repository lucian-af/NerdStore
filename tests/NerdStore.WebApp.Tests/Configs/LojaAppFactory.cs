using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Data.Context;

namespace NerdStore.WebApp.Tests.Configs
{
	public class LojaAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseStartup<TStartup>();
			builder.UseEnvironment("Tests");
			builder.ConfigureServices(services =>
			{
				var serviceProvider = new ServiceCollection()
					.AddEntityFrameworkSqlServer()
					.BuildServiceProvider(true);

				services.AddDbContext<CatalogoContext>(options =>
				{
					options.UseInternalServiceProvider(serviceProvider);
				});
			});
		}
	}
}
