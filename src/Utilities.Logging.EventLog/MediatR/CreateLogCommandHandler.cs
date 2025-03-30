using MediatR;
using NLog;

namespace Utilities.Logging.EventLog.MediatR;

public class CreateLogCommandHandler(ILog logger) : IRequestHandler<CreateLogCommand>
{
	public Task Handle(CreateLogCommand request, CancellationToken cancellationToken)
	{
		switch (request.LogType)
		{
			case LogType.Debug:
				logger.Debug(request.Message);
				break;
			case LogType.Information:
				logger.Information(request.Message);
				break;
			case LogType.Warning:
				logger.Warning(request.Message);
				break;
			case LogType.Error:
				logger.Error(request.Message);
				break;
			default:
				string message = $"Log Type {request.LogType} not found.  Could not write message to Log:  {request.Message}";
				logger.Error(message);
				break;
		}

		return Task.CompletedTask;
	}
}