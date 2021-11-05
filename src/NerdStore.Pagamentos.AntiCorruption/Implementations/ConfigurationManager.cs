using System;
using System.Linq;
using NerdStore.Pagamentos.AntiCorruption.Interfaces;

namespace NerdStore.Pagamentos.AntiCorruption.Implementations
{
	public class ConfigurationManager : IConfigurationManager
	{
		public string GetValue(string node)
			=> new(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
				.Select(s => s[new Random().Next(s.Length)]).ToArray());
	}
}
