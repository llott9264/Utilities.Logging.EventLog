using NLog;

namespace Utilities.Logging.EventLog;

public class Log : ILog
{
	private readonly ILogger _logger;

	public Log()
	{
		_logger = LogManager.GetCurrentClassLogger();
	}

	public void Information(string message)
	{
		_logger.Info(message);
	}

	public void Warning(string message)
	{
		_logger.Warn(message);
	}

	public void Debug(string message)
	{
		_logger.Debug(message);
	}

	public void Error(string message)
	{
		_logger.Error(message);
	}
}