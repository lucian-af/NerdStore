using EventSourcing.Interfaces;
using EventSourcing.Repository;
using EventSourcing.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Catalogo.Data.Context;
using NerdStore.Catalogo.Data.Repositories;
using NerdStore.Catalogo.Domain.DomainServices;
using NerdStore.Catalogo.Domain.Events.Handlers;
using NerdStore.Catalogo.Domain.Events.Models;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Pagamentos.AntiCorruption.Implementations;
using NerdStore.Pagamentos.AntiCorruption.Interfaces;
using NerdStore.Pagamentos.Business.Events;
using NerdStore.Pagamentos.Business.Interfaces;
using NerdStore.Pagamentos.Business.Services;
using NerdStore.Pagamentos.Data;
using NerdStore.Pagamentos.Data.Repository;
using NerdStore.Vendas.Application.Commands.Handlers;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Events;
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

			// Event Sourcing
			services.AddSingleton<IEventStoreService, EventStoreService>();
			services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

			// Catalogo
			services.AddScoped<CatalogoContext>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IProdutoAppService, ProdutoAppService>();
			services.AddScoped<IEstoqueService, EstoqueService>();

			services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, PedidoIniciadoEventHandler>();

			// Vendas
			services.AddScoped<VendasContext>();
			services.AddScoped<IPedidoRepository, PedidoRepository>();
			services.AddScoped<IPedidoQueries, PedidoQueries>();

			services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, AdicionarItemPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, AplicarVoucherPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, AtualizarItemPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, CancelarProcessamentoPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, CancelarProcessamentoPedidoEstornarEstoqueCommandHandler>();
			services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, FinalizarPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, IniciarPedidoCommandHandler>();
			services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, RemoverItemPedidoCommandHandler>();

			services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PagamentoRealizadoEventHandler>();
			services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PagamentoRecusadoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEstoqueRejeitadoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoItemAdicionadoEventHandler>();
			services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoRascunhoIniciadoEventHandler>();

			// Pagamento
			services.AddScoped<IPagamentoRepository, PagamentoRepository>();
			services.AddScoped<IPagamentoService, PagamentoService>();
			services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
			services.AddScoped<IPayPalGateway, PayPalGateway>();
			services.AddScoped<IConfigurationManager, ConfigurationManager>();
			services.AddScoped<PagamentoContext>();

			services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();


		}
	}
}
