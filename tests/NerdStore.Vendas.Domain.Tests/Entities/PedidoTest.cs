using System;
using NerdStore.Vendas.Domain.Entidades;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests.Entities
{
	public class PedidoTest
	{
		[Fact]
		public void Pedido_Validar_CalcuarValorPedido_ValorTotal_Deve_Ser_Igual_Soma_Dos_Itens()
		{
			// Arrange
			var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

			pedido.AdicionarItem(new PedidoItem(
				Guid.NewGuid(),
				"Teste",
				 3,
				 33.33m));

			// Act
			pedido.CalcularValorPedido();

			// Assert
			Assert.Equal(99.99m, pedido.ValorTotal);
		}

		[Fact]
		public void Pedido_Validar_AplicarVoucher_DataValidadeExpirada_DeveRetornarErros()
		{
			// Arrange && Act && Assert
			var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

			pedido.AdicionarItem(new PedidoItem(
				Guid.NewGuid(),
				"Teste",
				 3,
				 33.33m));

			var result = pedido.AplicarVoucher(new Voucher(
				   "TST-01",
				   0,
				   0,
				   0,
				   Enums.TipoDescontoVoucher.Valor,
				   DateTime.Now.AddDays(-1)
				));

			var erros = string.Join("| ", result.Errors);

			Assert.False(result.IsValid);
			Assert.Contains("Voucher expirado", erros);
			Assert.Contains("Voucher inválido.", erros);
			Assert.Contains("Voucher indisponível.", erros);
		}

		[Fact]
		public void Pedido_Validar_AplicarVoucher_DataValidadeDisponivel_DeveAplicarVoucher()
		{
			// Arrange && Act && Assert
			var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

			pedido.AdicionarItem(new PedidoItem(
				Guid.NewGuid(),
				"Teste",
				 3,
				 33.33m));

			var result = pedido.AplicarVoucher(new Voucher(
				   "TST-01",
				   0,
				   10,
				   1,
				   Enums.TipoDescontoVoucher.Valor,
				   DateTime.Now.AddDays(1)
				));

			Assert.True(result.IsValid);
			Assert.False(pedido.Voucher.Ativo);
			Assert.True(pedido.Voucher.Utilizado);
			Assert.Equal(0, pedido.Voucher.Quantidade);
			Assert.Equal(DateTime.Now.Date, pedido.Voucher.DataUtilizacao.Value.Date);
			Assert.Equal(89.99m, pedido.ValorTotal);
		}
	}
}
