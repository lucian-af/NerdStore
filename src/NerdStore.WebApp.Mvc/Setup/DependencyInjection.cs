using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.DomainServices;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Handlers;
using NerdStore.Core.Interfaces;

namespace NerdStore.WebApp.Mvc.Setup
{
	public static class DependencyInjection
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			// Domain Bus (Mediator)
			services.AddScoped<IMediatrHandler, MediatrHandler>();

			// Catalogo
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IProdutoAppService, ProdutoAppService>();
			services.AddScoped<IEstoqueService, EstoqueService>();
			services.AddScoped<CatalogoContext>();

			services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
		}
	}
}
