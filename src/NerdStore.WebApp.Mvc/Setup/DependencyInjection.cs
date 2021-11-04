using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.DomainServices;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Commands.Handlers;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Events.Handlers;
using NerdStore.Vendas.Application.Events.Models;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Application.Queries.Dtos.Interfaces;
using NerdStore.Vendas.Data.Context;
using NerdStore.Vendas.Data.Repository;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.WebApp.Mvc.Setup
{
	public static class DependencyInjection
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			// Mediator
			services.AddScoped<IMediatorHandler, MediatorHandler>();

			// Notifications
			services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

			// Catalogo
			services.AddScoped<CatalogoContext>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IProdutoAppService, ProdutoAppService>();
			services.AddScoped<IEstoqueService, EstoqueService>();

			services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

			// Vendas
			services.AddScoped<VendasContext>();
			services.AddScoped<IPedidoRepository, PedidoRepository>();
			services.AddScoped<IPedidoQueries, PedidoQueries>();

			services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
			services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, AtualizarItemPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, RemoverItemPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, AplicarVoucherPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, IniciarPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, FinalizarPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, CancelarProcessamentoPedidoCommandHandler>();

			services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();

		}
	}
}
