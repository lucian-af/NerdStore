using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Data.Mapping
{
	public class CategoriaMap : IEntityTypeConfiguration<Categoria>
	{
		public void Configure(EntityTypeBuilder<Categoria> builder)
		{
			builder.ToTable("Categoria")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdCategoria");

			builder.Property(c => c.Nome)
				.HasColumnName("Nome")
				.HasColumnType("varchar(250)")
				.IsRequired();

			builder.Property(c => c.Codigo)
				.HasColumnName("Codigo")
				.HasColumnType("int");

			// 1 : N => Categorias : Produtos
			builder.HasMany(c => c.Produtos)
				.WithOne(p => p.Categoria)
				.HasForeignKey(p => p.IdCategoria);
		}
	}
}
