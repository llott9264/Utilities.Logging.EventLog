using MediatR;

namespace Utilities.Logging.EventLog.MediatR;

public class CreateLogCommand(string message, LogType logType) : IRequest
{
	public string Message { get; set; } = message;
	public LogType LogType { get; set; } = logType;
}