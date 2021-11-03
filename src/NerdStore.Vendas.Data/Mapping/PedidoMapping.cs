﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Domain.Entidades;

namespace NerdStore.Vendas.Data.Mappings
{
	public class PedidoMapping : IEntityTypeConfiguration<Pedido>
	{
		public void Configure(EntityTypeBuilder<Pedido> builder)
		{
			builder
				.ToTable("Pedido")
				.HasKey(c => c.Id);

			builder.Property(c => c.Id)
				.HasColumnName("IdPedido");

			builder.Property(c => c.Codigo)
				.HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

			// 1 : N => Pedido : PedidoItems
			builder.HasMany(c => c.PedidoItems)
				.WithOne(c => c.Pedido)
				.HasForeignKey(c => c.IdPedido);

		}
	}
}
