﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Pagamentos.Data;

namespace NerdStore.Catalogo.Data.Context
{
	public class PagamentoContextFactory : IDesignTimeDbContextFactory<PagamentoContext>
	{
		public PagamentoContext CreateDbContext(string[] args)
		{
			IMediatorHandler _mediatorHandler = null;

			IConfiguration config = new ConfigurationBuilder()
								.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NerdStore.WebApp.Mvc"))
								.AddJsonFile("appsettings.json")
								.Build();

			var connectionString = config.GetConnectionString("DefaultConnection");

			if (string.IsNullOrEmpty(connectionString))
				throw new DbUpdateException("String de conexão 'DefaultConnection' não está configurada.");

			var optionsBuilder = new DbContextOptionsBuilder<PagamentoContext>();
			optionsBuilder.UseSqlServer(connectionString,
				  c => c.MigrationsAssembly("NerdStore.Pagamentos.Data"));

			optionsBuilder.UseSqlServer(connectionString);

			return new PagamentoContext(optionsBuilder.Options, _mediatorHandler);
		}
	}
}
