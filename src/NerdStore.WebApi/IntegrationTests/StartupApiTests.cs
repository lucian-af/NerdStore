using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Core.Settings;
using NerdStore.Vendas.Data.Context;
using NerdStore.WebApi.Setup;
using NerdStore.WebApp.Mvc.Setup;

namespace NerdStore.WebApi.IntegrationTests
{
	public class StartupApiTests
	{
		public IConfiguration Configuration { get; }

		public StartupApiTests(IHostEnvironment hostEnvironment)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(hostEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<CatalogoContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<VendasContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.LoadAppSettings(Configuration);

			// JWT

			var key = Encoding.ASCII.GetBytes(AuthenticationSettings.Secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = AuthenticationSettings.ValidoEm,
					ValidIssuer = AuthenticationSettings.Emissor
				};
			});

			services.AddMvc();

			services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));

			services.AddMediatR(typeof(Startup));

			services.RegisterServices();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
