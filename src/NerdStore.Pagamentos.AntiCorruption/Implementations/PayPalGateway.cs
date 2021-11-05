﻿using System;
using System.Linq;
using NerdStore.Pagamentos.AntiCorruption.Interfaces;

namespace NerdStore.Pagamentos.AntiCorruption.Implementations
{
	public class PayPalGateway : IPayPalGateway
	{
		public bool CommitTransaction(string cardHashKey, string orderId, decimal amount)
			=> new Random().Next(2) == 0; // método aleatório, vai simular recusa de transação

		public string GetCardHashKey(string serviceKey, string cartaoCredito)
			=> new(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
				.Select(s => s[new Random().Next(s.Length)]).ToArray());

		public string GetPayPalServiceKey(string apiKey, string encriptionKey)
			=> new(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
				.Select(s => s[new Random().Next(s.Length)]).ToArray());
	}
}
