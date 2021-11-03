using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Data.Mapping
{
	public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
	{
		public void Configure(EntityTypeBuilder<PedidoItem> builder)
		{
			builder
				.ToTable("PedidoItem")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdPedidoItem");

			builder.Property(c => c.NomeProduto)
				.HasColumnType("varchar(250)")
				.IsRequired();

			// 1 : N => Pedido : Pagamento
			builder.HasOne(c => c.Pedido)
				.WithMany(c => c.PedidoItems);

		}
	}
}
