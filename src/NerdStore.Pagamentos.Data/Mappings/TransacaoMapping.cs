using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Pagamentos.Business.Entidades;

namespace NerdStore.Pagamentos.Data.Mappings
{
	public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
	{
		public void Configure(EntityTypeBuilder<Transacao> builder)
		{
			builder
				.ToTable("Transacao")
				.HasKey(c => c.Id);

			builder
				.Property(t => t.Id)
				.HasColumnName("IdTransacao");
		}
	}
}
