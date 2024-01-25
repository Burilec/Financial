namespace Burile.Financial.CronJobs;

internal static class Program
{
    public static Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                       .BaseWorkerConfiguration()
                       .Build();

        return host.RunAsync();
    }
}