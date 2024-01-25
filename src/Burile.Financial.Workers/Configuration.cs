using Quartz;

namespace Burile.Financial.Workers;

internal static class Configuration
{
    internal static IHostBuilder BaseWorkerConfiguration(this IHostBuilder builder)
        => builder.ConfigureServices(static services =>
        {
            services.AddHostedService<Worker>();
            services.AddQuartz(static q =>
            {
            });
            services.AddQuartzHostedService(static opt =>
            {
                opt.WaitForJobsToComplete = true;
            });
        });
}