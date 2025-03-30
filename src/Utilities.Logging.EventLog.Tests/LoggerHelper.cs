using NLog;
using NLog.Config;
using NLog.Targets;

namespace Utilities.Logging.EventLog.Tests
{
	internal class LoggerHelper
	{
		internal class LoggerConfigurationMock
		{
			private readonly MemoryTarget _memoryTarget = new() { Name = "mem" };

			public IList<string> Logs => _memoryTarget.Logs;

			internal LoggingConfiguration GetLogConfiguration()
			{
				LoggingConfiguration configuration = new();
				configuration.AddRule(LogLevel.Debug, LogLevel.Fatal, _memoryTarget);
				return configuration;
			}

		}
	}
}
