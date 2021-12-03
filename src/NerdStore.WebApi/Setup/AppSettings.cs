using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Interfaces;
using NerdStore.Core.Settings;

namespace NerdStore.WebApp.Mvc.Setup
{
	public static class AppSettings
	{

		public static void LoadAppSettings(this IServiceCollection services, IConfiguration configuration)
		{
			var settings = new List<ISettings>
			{
				 new CatalogoSettings(),
				 new AuthenticationSettings()
			};

			foreach (var setting in settings)
			{
				configuration.Bind(setting.ToString(), setting);
				services.AddSingleton(setting.GetType(), setting);
			}
		}
	}
}
