using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.Mvc.Extensions
{
	public class CartViewComponent : ViewComponent
	{
		//private readonly IPedidoQueries _pedidoQueries;

		//// TODO: Obter cliente logado
		protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

		public CartViewComponent()
		{
		}

		//public CartViewComponent(IPedidoQueries pedidoQueries)
		//{
		//    _pedidoQueries = pedidoQueries;
		//}

		public async Task<IViewComponentResult> InvokeAsync() =>
			//var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
			//var itens = carrinho?.Items.Count ?? 0;			
			View();
	}
}
