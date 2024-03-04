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