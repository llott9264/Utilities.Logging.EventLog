using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Logging.EventLog.MediatR;

namespace Utilities.Logging.EventLog.Tests;

public class LoggerServiceRegistrationTests
{
	private readonly IConfiguration _configuration = new ConfigurationBuilder()
		.AddInMemoryCollection(new Dictionary<string, string?>
		{
			{ "NLogConfigFile", "Value1" }
		})
		.Build();

	[Fact]
	public void AddLoggerServices_RegistersAllServices_CorrectlyResolvesTypes()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddLoggerServices(_configuration);
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		IMediator? mediator = serviceProvider.GetService<IMediator>();
		ILog? gpg = serviceProvider.GetService<ILog>();

		// Assert
		Assert.NotNull(mediator);
		_ = Assert.IsType<Mediator>(mediator);

		Assert.NotNull(gpg);
		_ = Assert.IsType<Log>(gpg);
	}

	[Fact]
	public void AddLoggerServices_ReturnsServiceCollection()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		IServiceCollection result = services.AddLoggerServices(_configuration);

		// Assert
		Assert.Same(services, result); // Ensures the method returns the same IServiceCollection
	}

	[Fact]
	public void AddLoggerServices_ScopedLifetime_VerifyInstanceWithinScope()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddLoggerServices(_configuration);
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		using IServiceScope scope = serviceProvider.CreateScope();
		IMediator? service1 = scope.ServiceProvider.GetService<IMediator>();
		IMediator? service2 = scope.ServiceProvider.GetService<IMediator>();
		ILog? service3 = scope.ServiceProvider.GetService<ILog>();
		ILog? service4 = scope.ServiceProvider.GetService<ILog>();

		Assert.NotSame(service1, service2);
		Assert.Same(service3, service4);
	}

	[Fact]
	public void AddLoggerServices_ScopedLifetime_VerifyInstancesAcrossScopes()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddLoggerServices(_configuration);
		ServiceProvider serviceProvider = services.BuildServiceProvider();

		// Assert
		IMediator? service1, service2;
		ILog? service3, service4;
		using (IServiceScope scope1 = serviceProvider.CreateScope())
		{
			service1 = scope1.ServiceProvider.GetService<IMediator>();
			service3 = scope1.ServiceProvider.GetService<ILog>();
		}

		using (IServiceScope scope2 = serviceProvider.CreateScope())
		{
			service2 = scope2.ServiceProvider.GetService<IMediator>();
			service4 = scope2.ServiceProvider.GetService<ILog>();
		}

		Assert.NotSame(service1, service2);
		Assert.Same(service3, service4);
	}

	[Fact]
	public void AddLoggerServices_CreateLogCommandHandler_VerifyMediatorHandlerExists()
	{
		// Arrange
		ServiceCollection services = new();

		// Act
		_ = services.AddLoggerServices(_configuration);
		List<ServiceDescriptor> serviceDescriptors = services.ToList();

		// Assert
		ServiceDescriptor? handlerDescriptor = serviceDescriptors.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IRequestHandler<CreateLogCommand>));

		Assert.NotNull(handlerDescriptor);
		Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
	}
}