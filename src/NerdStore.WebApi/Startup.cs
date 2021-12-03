using System;
using System.IO;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NerdStore.Catalogo.Application.AutoMapper;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Core.Settings;
using NerdStore.Pagamentos.Data;
using NerdStore.Vendas.Data.Context;
using NerdStore.WebApi.Setup;
using NerdStore.WebApp.Mvc.Setup;

namespace NerdStore.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.LoadAppSettings(Configuration);

			services.AddControllers();

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

			services.AddSwaggerGen(options =>
			{
				options.UseInlineDefinitionsForEnums();

				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "NerdStoreAPI"
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Token de Autorização:",
					Name = "Authorization",
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});

			var connetionStrings = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<CatalogoContext>(options => options.UseSqlServer(connetionStrings));
			services.AddDbContext<VendasContext>(options => options.UseSqlServer(connetionStrings));
			services.AddDbContext<PagamentoContext>(options => options.UseSqlServer(connetionStrings));

			services.AddAutoMapper(
				typeof(DomainToDtoMappingProfile),
				typeof(DtoToDomainMappingProfile)
				);

			services.AddMediatR(typeof(Startup));			

			services.RegisterServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					options.DisplayRequestDuration();
					options.SwaggerEndpoint("v1/swagger.json", "NerdStoreAPI v1");
				});

				app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
