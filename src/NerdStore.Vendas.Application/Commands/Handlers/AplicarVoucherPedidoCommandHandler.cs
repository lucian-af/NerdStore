using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Events.Models;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
	public class AplicarVoucherPedidoCommandHandler : CommandHandler, IRequestHandler<AplicarVoucherPedidoCommand, bool>
	{
		private readonly IPedidoRepository _repoPedido;

		public AplicarVoucherPedidoCommandHandler(
			IMediatorHandler mediatorHandler,
			IPedidoRepository repoPedido) : base(mediatorHandler)
			=> _repoPedido = repoPedido;

		public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
		{
			if (!ValidarComando(message))
				return false;

			var pedido = await _repoPedido.ObterPedidoRascunhoPorIdCliente(message.IdCliente);

			if (pedido == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
				return false;
			}

			var voucher = await _repoPedido.ObterVoucherPorCodigo(message.CodigoVoucher);

			if (voucher == null)
			{
				await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Voucher não encontrado!"));
				return false;
			}

			var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);
			if (!voucherAplicacaoValidation.IsValid)
			{
				foreach (var error in voucherAplicacaoValidation.Errors)
				{
					await _mediatorHandler.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));
				}

				return false;
			}

			pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(message.IdCliente, pedido.Id, voucher.Id));

			_repoPedido.Atualizar(pedido);

			return await _repoPedido.UnitOfWork.Commit();
		}
	}
}
