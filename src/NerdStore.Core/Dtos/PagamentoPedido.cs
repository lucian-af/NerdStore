﻿using System;

namespace NerdStore.Core.Dtos
{
	public class PagamentoPedido
	{
		public Guid IdPedido { get; set; }
		public Guid IdCliente { get; set; }
		public decimal Total { get; set; }
		public string NomeCartao { get; set; }
		public string NumeroCartao { get; set; }
		public string ExpiracaoCartao { get; set; }
		public string CvvCartao { get; set; }
	}
}
