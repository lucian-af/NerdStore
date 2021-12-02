using System;
using System.Linq;

namespace NerdStore.WebApp.Mvc.IntegrationTests.Configs
{
	public static class TestsExtensions
	{
		public static decimal ApenasNumeros(this string value)
			=> Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));
	}
}
