using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Vendas.Data.Context;
using NerdStore.WebApp.Mvc.Setup;

namespace NerdStore.WebApp.Mvc
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
			=> Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			var connetionStrings = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<CatalogoContext>(options => options.UseSqlServer(connetionStrings));
			services.AddDbContext<VendasContext>(options => options.UseSqlServer(connetionStrings));

			services.AddAutoMapper(
				typeof(DomainToDtoMappingProfile),
				typeof(DtoToDomainMappingProfile)
				);

			services.AddMediatR(typeof(Startup));

			services.LoadAppSettings(Configuration);

			services.RegisterServices();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
