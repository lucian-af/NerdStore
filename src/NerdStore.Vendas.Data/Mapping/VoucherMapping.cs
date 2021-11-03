using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Data.Mappings
{
	public class VoucherMapping : IEntityTypeConfiguration<Voucher>
	{
		public void Configure(EntityTypeBuilder<Voucher> builder)
		{
			builder
				.ToTable("Voucher")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdVoucher");

			builder.Property(c => c.Codigo)
				.HasColumnType("varchar(100)")
				.IsRequired();

			// 1 : N => Voucher : Pedidos
			builder.HasMany(c => c.Pedidos)
				.WithOne(c => c.Voucher)
				.HasForeignKey(c => c.IdVoucher);

		}
	}
}
