using Moq;
using Utilities.Logging.EventLog.MediatR;

namespace Utilities.Logging.EventLog.Tests;

public class MediatRTests
{
	[Fact]
	public void CreateLogCommandHandler_LogTypeInformation_CallsInformationMethod()
	{
		//Arrange
		Mock<ILog> mock = new();
		CreateLogCommand request = new("Hello World!", LogType.Information);
		CreateLogCommandHandler handler = new(mock.Object);

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(l => l.Information(It.IsAny<string>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}

	[Fact]
	public void CreateLogCommandHandler_LogTypeWarning_CallsWarningMethod()
	{
		//Arrange
		Mock<ILog> mock = new();

		//Act
		CreateLogCommand request = new("Hello World!", LogType.Warning);
		CreateLogCommandHandler handler = new(mock.Object);
		handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(l => l.Warning(It.IsAny<string>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}

	[Fact]
	public void CreateLogCommandHandler_LogTypeError_CallsErrorMethod()
	{
		//Arrange
		Mock<ILog> mock = new();
		CreateLogCommand request = new("Hello World!", LogType.Error);
		CreateLogCommandHandler handler = new(mock.Object);

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(l => l.Error(It.IsAny<string>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}

	[Fact]
	public void CreateLogCommandHandler_LogTypeDebug_CallsDebugMethod()
	{
		//Arrange
		Mock<ILog> mock = new();
		CreateLogCommand request = new("Hello World!", LogType.Debug);
		CreateLogCommandHandler handler = new(mock.Object);

		//Act
		handler.Handle(request, CancellationToken.None);

		//Assert
		mock.Verify(l => l.Debug(It.IsAny<string>()), Times.Once);
		mock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task Handle_InvalidLogType_CallsErrorWithNotFoundMessage()
	{
		// Arrange
		Mock<ILog> mock = new();
		CreateLogCommand request = new("Hello World!", (LogType)999);
		CreateLogCommandHandler handler = new(mock.Object);

		// Act
		await handler.Handle(request, CancellationToken.None);

		// Assert
		mock.Verify(l => l.Error(It.IsAny<string>()), Times.Once());
		mock.VerifyNoOtherCalls();
	}
}