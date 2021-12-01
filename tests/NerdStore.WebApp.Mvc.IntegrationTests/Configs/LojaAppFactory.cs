using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NerdStore.WebApp.Mvc.IntegrationTests.Configs
{
	public class LojaAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseStartup<TStartup>();
			builder.UseEnvironment("testing");
		}
	}
}
