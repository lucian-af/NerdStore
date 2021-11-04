﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Queries.Dtos;
using NerdStore.Vendas.Application.Queries.Dtos.Interfaces;

namespace NerdStore.WebApp.MVC.Controllers
{
	public class CarrinhoController : ControllerBase
	{
		private readonly IProdutoAppService _produtoAppService;
		private readonly IPedidoQueries _pedidoQueries;
		private readonly IMediatorHandler _mediatorHandler;

		public CarrinhoController(INotificationHandler<DomainNotification> notifications,
								  IProdutoAppService produtoAppService,
								  IMediatorHandler mediatorHandler,
								  IPedidoQueries pedidoQueries) : base(notifications, mediatorHandler)
		{
			_produtoAppService = produtoAppService;
			_mediatorHandler = mediatorHandler;
			_pedidoQueries = pedidoQueries;
		}

		[Route("meu-carrinho")]
		public async Task<IActionResult> Index()
			=> View(await _pedidoQueries.ObterCarrinhoCliente(IdCliente));

		[HttpPost]
		[Route("meu-carrinho")]
		public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
		{
			var produto = await _produtoAppService.ObterPorId(id);
			if (produto == null) return BadRequest();

			if (produto.QuantidadeEstoque < quantidade)
			{
				TempData["Erro"] = "Produto com estoque insuficiente";
				return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
			}

			var command = new AdicionarItemPedidoCommand(IdCliente, produto.Id, produto.Nome, quantidade, produto.Valor);
			await _mediatorHandler.EnviarComando(command);

			if (OperacaoValida())
			{
				return RedirectToAction("Index");
			}

			TempData["Erros"] = ObterMensagensErro();
			return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
		}

		[HttpPost]
		[Route("remover-item")]
		public async Task<IActionResult> RemoverItem(Guid id)
		{
			var produto = await _produtoAppService.ObterPorId(id);
			if (produto == null) return BadRequest();

			var command = new RemoverItemPedidoCommand(IdCliente, id);
			await _mediatorHandler.EnviarComando(command);

			if (OperacaoValida())
			{
				return RedirectToAction("Index");
			}

			return View("Index", await _pedidoQueries.ObterCarrinhoCliente(IdCliente));
		}

		[HttpPost]
		[Route("atualizar-item")]
		public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
		{
			var produto = await _produtoAppService.ObterPorId(id);
			if (produto == null) return BadRequest();

			var command = new AtualizarItemPedidoCommand(IdCliente, id, quantidade);
			await _mediatorHandler.EnviarComando(command);

			if (OperacaoValida())
			{
				return RedirectToAction("Index");
			}

			return View("Index", await _pedidoQueries.ObterCarrinhoCliente(IdCliente));
		}

		[HttpPost]
		[Route("aplicar-voucher")]
		public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
		{
			var command = new AplicarVoucherPedidoCommand(IdCliente, voucherCodigo);
			await _mediatorHandler.EnviarComando(command);

			if (OperacaoValida())
			{
				return RedirectToAction("Index");
			}

			return View("Index", await _pedidoQueries.ObterCarrinhoCliente(IdCliente));
		}

		[Route("resumo-da-compra")]
		public async Task<IActionResult> ResumoDaCompra()
			=> View(await _pedidoQueries.ObterCarrinhoCliente(IdCliente));

		[HttpPost]
		[Route("iniciar-pedido")]
		public async Task<IActionResult> IniciarPedido(CarrinhoDto carrinhoDto)
		{
			var carrinho = await _pedidoQueries.ObterCarrinhoCliente(IdCliente);

			var command = new IniciarPedidoCommand(carrinho.IdPedido, IdCliente, carrinho.ValorTotal, carrinhoDto.Pagamento.NomeCartao,
				carrinhoDto.Pagamento.NumeroCartao, carrinhoDto.Pagamento.ExpiracaoCartao, carrinhoDto.Pagamento.CvvCartao);

			await _mediatorHandler.EnviarComando(command);

			if (OperacaoValida())
			{
				return RedirectToAction("Index", "Pedido");
			}

			return View("ResumoDaCompra", await _pedidoQueries.ObterCarrinhoCliente(IdCliente));
		}
	}
}
