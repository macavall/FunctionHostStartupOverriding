using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication()
            .ConfigureServices(services =>
            {
                services.AddApplicationInsightsTelemetryWorkerService();
                services.ConfigureFunctionsApplicationInsights();
            })
            .Build();

        host.Run();
    }
}

public interface IMyService
{
    public void MyServiceMethod();
}

public class MyService : IMyService
{
    public IServiceCollection? _serviceCollection;
    public IServiceProvider? _serviceProvider;

    public MyService()
    {
        this.CreateServiceCollection();
        // Use AddHttpClient here or in the MyService class
    }

    public void CreateServiceCollection()
    {
        _serviceCollection = new ServiceCollection();

        _serviceCollection.AddHttpClient();
        _serviceCollection.AddAuthentication();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    public void MyServiceMethod()
    {
        var myService = _serviceProvider?.GetRequiredService<IHttpClientFactory>();

        var myClient = myService.CreateClient();

        System.Console.WriteLine("Hello from MyServiceMethod()!");
    }
}