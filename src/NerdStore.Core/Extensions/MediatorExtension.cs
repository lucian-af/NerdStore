using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Extensions
{
	public static class MediatorExtension
	{
		public static async Task PublicarEventos(this IMediatorHandler mediator, DbContext ctx)
		{
			var domainEntities = ctx.ChangeTracker
				.Entries<Entidade>()
				.Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

			var domainEvents = domainEntities
				.SelectMany(x => x.Entity.Notificacoes)
				.ToList();

			domainEntities.ToList()
				.ForEach(entity => entity.Entity.LimparEventos());

			var tasks = domainEvents
				.Select(async (domainEvent) =>
				{
					await mediator.PublicarEvento(domainEvent);
				});

			await Task.WhenAll(tasks);
		}
	}
}
