using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Pagamentos.Business.Entidades;

namespace NerdStore.Pagamentos.Data.Mappings
{
	public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
	{
		public void Configure(EntityTypeBuilder<Pagamento> builder)
		{
			builder
				.ToTable("Pagamento")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdPagamento");

			builder.Property(c => c.NomeCartao)
				.HasColumnType("varchar(250)")
				.IsRequired();

			builder.Property(c => c.NumeroCartao)
				.HasColumnType("varchar(16)")
				.IsRequired();

			builder.Property(c => c.ExpiracaoCartao)
				.HasColumnType("varchar(10)")
				.IsRequired();

			builder.Property(c => c.CvvCartao)
				.HasColumnType("varchar(4)")
				.IsRequired();

			// 1 : 1 => Pagamento : Transacao
			builder.HasOne(c => c.Transacao)
				.WithOne(c => c.Pagamento)
				.HasForeignKey<Transacao>(p => p.IdPagamento);

		}
	}
}
