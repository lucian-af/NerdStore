using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Vendas.Application.Queries.Dtos.Interfaces;

namespace NerdStore.WebApp.Mvc.Controllers
{
	public class PedidoController : ControllerBase
	{
		private readonly IPedidoQueries _pedidoQueries;

		public PedidoController(IPedidoQueries pedidoQueries,
			INotificationHandler<DomainNotification> notifications,
			IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
			=> _pedidoQueries = pedidoQueries;

		[Route("meus-pedidos")]
		public async Task<IActionResult> Index()
			=> View(await _pedidoQueries.ObterPedidosCliente(IdCliente));
	}
}
