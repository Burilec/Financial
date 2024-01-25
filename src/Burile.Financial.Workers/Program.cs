namespace Burile.Financial.Workers;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                       .BaseWorkerConfiguration()
                       .Build();

        await host.RunAsync();
    }
}