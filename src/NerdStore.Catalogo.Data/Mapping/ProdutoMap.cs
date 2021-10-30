using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Data.Mapping
{
	public class ProdutoMap : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.ToTable("Produto")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdProduto");

			builder.Property(c => c.Nome)
				.HasColumnName("Nome")
				.HasColumnType("varchar(250)")
				.IsRequired();

			builder.Property(c => c.Descricao)
				.HasColumnName("Descricao")
				.HasColumnType("varchar(500)")
				.IsRequired();

			builder.Property(c => c.Imagem)
				.HasColumnName("Imagem")
				.HasColumnType("varchar(250)")
				.IsRequired();

			builder.OwnsOne(c => c.Dimensoes, cm =>
			{
				cm.Property(c => c.Altura)
					.HasColumnName("Altura")
					.HasColumnType("decimal");

				cm.Property(c => c.Largura)
					.HasColumnName("Largura")
					.HasColumnType("decimal");

				cm.Property(c => c.Profundidade)
					.HasColumnName("Profundidade")
					.HasColumnType("decimal");
			});
		}
	}
}
