using NLog;

namespace Utilities.Logging.EventLog.Tests
{
	public class LoggerTests
	{
		private const string Message = "Hello World!";

		[Fact]
		public void WriteInfoLog_ValidString_ReturnsLogCountEqualOne()
		{
			//Arrange
			LoggerHelper.LoggerConfigurationMock loggerConfigurationMock = new();
			LogManager.Configuration = loggerConfigurationMock.GetLogConfiguration();
			ILog logger = new Log();

			//Act
			logger.Information(Message);
			IList<string> logs = loggerConfigurationMock.Logs;
			string[]? fields = logs.FirstOrDefault()?.Split('|');

			//Assert
			Assert.True(logs.Count == 1);
			Assert.NotNull(fields);
			Assert.True(fields[1] == "INFO");
			Assert.Contains(Message, fields[3]);
		}

		[Fact]
		public void WriteDebugLog_ValidString_ReturnsLogCountEqualOne()
		{
			//Arrange
			LoggerHelper.LoggerConfigurationMock loggingConfigurationMock = new();
			LogManager.Configuration = loggingConfigurationMock.GetLogConfiguration();
			ILog logger = new Log();

			//Act
			logger.Debug(Message);
			IList<string> logs = loggingConfigurationMock.Logs;
			string[]? fields = logs.FirstOrDefault()?.Split('|');

			//Assert
			Assert.True(logs.Count == 1);
			Assert.NotNull(fields);
			Assert.True(fields[1] == "DEBUG");
			Assert.Contains(Message, fields[3]);
		}

		[Fact]
		public void WriteWarningLog_ValidString_ReturnsLogCountEqualOne()
		{
			//Arrange
			LoggerHelper.LoggerConfigurationMock loggingConfigurationMock = new();
			LogManager.Configuration = loggingConfigurationMock.GetLogConfiguration();
			ILog logger = new Log();

			//Act
			logger.Warning(Message);
			IList<string> logs = loggingConfigurationMock.Logs;
			string[]? fields = logs.FirstOrDefault()?.Split('|');

			//Assert
			Assert.True(logs.Count == 1);
			Assert.NotNull(fields);
			Assert.True(fields[1] == "WARN");
			Assert.Contains(Message, fields[3]);
		}

		[Fact]
		public void WriteErrorLog_ValidString_ReturnsLogCountEqualOne()
		{
			//Arrange
			LoggerHelper.LoggerConfigurationMock loggingConfigurationMock = new();
			LogManager.Configuration = loggingConfigurationMock.GetLogConfiguration();
			ILog logger = new Log();

			//Act
			logger.Error(Message);
			IList<string> logs = loggingConfigurationMock.Logs;
			string[]? fields = logs.FirstOrDefault()?.Split('|');

			//Assert
			Assert.True(logs.Count == 1);
			Assert.NotNull(fields);
			Assert.True(fields[1] == "ERROR");
			Assert.Contains(Message, fields[3]);
		}
	}
}
