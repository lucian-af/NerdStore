using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NerdStore.WebApp.Tests.Configs
{
	public static class TestsExtensions
	{
		public static decimal NumberOnly(this string value)
			=> Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));

		public static void AddToken(this HttpClient client, string token, string scheme = "Bearer")
		{
			client.AddJsonMediaType();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token.Replace("Bearer", ""));
		}

		public static void AddJsonMediaType(this HttpClient client)
		{
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
	}
}
