namespace NerdStore.Core.Settings
{
	public class AuthenticationSettings : Settings
	{
		public static string Secret { get; set; }
		public static int ExpiracaoHoras { get; set; }
		public static string Emissor { get; set; }
		public static string ValidoEm { get; set; }
	}
}
