﻿namespace Utilities.Logging.EventLog;

public interface ILog
{
	void Information(string message);
	void Warning(string message);
	void Debug(string message);
	void Error(string message);

}