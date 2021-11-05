using System.Threading.Tasks;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Dtos;
using NerdStore.Core.Messages.Common.IntegrationEvents;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Pagamentos.Business.Dtos;
using NerdStore.Pagamentos.Business.Entidades;
using NerdStore.Pagamentos.Business.Interfaces;
using NerdStore.Pagamentos.Business.Models.Enums;

namespace NerdStore.Pagamentos.Business.Services
{
	public class PagamentoService : IPagamentoService
	{
		private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
		private readonly IPagamentoRepository _pagamentoRepository;
		private readonly IMediatorHandler _mediatorHandler;

		public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
								IPagamentoRepository pagamentoRepository,
								IMediatorHandler mediatorHandler)
		{
			_pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
			_pagamentoRepository = pagamentoRepository;
			_mediatorHandler = mediatorHandler;
		}

		public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
		{
			var pedido = new Pedido
			{
				Id = pagamentoPedido.IdPedido,
				Valor = pagamentoPedido.Total
			};

			var pagamento = new Pagamento
			{
				Valor = pagamentoPedido.Total,
				NomeCartao = pagamentoPedido.NomeCartao,
				NumeroCartao = pagamentoPedido.NumeroCartao,
				ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
				CvvCartao = pagamentoPedido.CvvCartao,
				IdPedido = pagamentoPedido.IdPedido
			};

			var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

			if (transacao.StatusTransacao == StatusTransacao.Pago)
			{
				pagamento.AdicionarEvento(
					   new PagamentoRealizadoEvent(
						   pedido.Id,
						   pagamentoPedido.IdCliente,
						   transacao.IdPagamento,
						   transacao.Id,
						   pedido.Valor));

				_pagamentoRepository.Adicionar(pagamento);
				_pagamentoRepository.AdicionarTransacao(transacao);

				await _pagamentoRepository.UnitOfWork.Commit();
				return transacao;
			}

			await _mediatorHandler.PublicarNotificacao(
				new DomainNotification("pagamento", "A operadora recusou o pagamento"));

			await _mediatorHandler.PublicarEvento(new
				PagamentoRecusadoEvent(pedido.Id, pagamentoPedido.IdCliente, transacao.IdPagamento, transacao.Id, pedido.Valor));

			return transacao;
		}
	}
}
