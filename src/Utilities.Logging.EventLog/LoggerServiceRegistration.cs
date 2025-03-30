using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Utilities.Logging.EventLog;

public static class LoggerServiceRegistration
{
	public static IServiceCollection AddLoggerServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
		LogManager.Setup().LoadConfigurationFromFile(configuration.GetValue<string>("NLogConfigFile"));
		services.AddSingleton<ILog, Log>();
		return services;
	}
}