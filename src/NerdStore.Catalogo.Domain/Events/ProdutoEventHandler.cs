using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Catalogo.Domain.Interfaces;

namespace NerdStore.Catalogo.Domain.Events
{
	public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
	{
		private readonly IProdutoRepository _repoProduto;

		public ProdutoEventHandler(IProdutoRepository repoProduto)
			=> _repoProduto = repoProduto;

		/// <summary>
		/// Isso é só um exemplo
		/// Poderia ser implementado envio de e-mail para notificar alguém que o produto está com o estoque abaixo, etc.
		/// </summary>
		/// <param name="notification"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
			=> await _repoProduto.ObterPorId(notification.IdAggregate);
	}
}
