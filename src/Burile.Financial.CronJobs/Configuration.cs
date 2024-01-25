using Burile.Financial.CronJobs.Features.ProduceMessageToUpdateProducts;
using Burile.Financial.Infrastructure.Jobs;
using Quartz;

namespace Burile.Financial.CronJobs;

internal static class Configuration
{
    internal static IHostBuilder BaseWorkerConfiguration(this IHostBuilder builder)
        => builder.ConfigureServices(static (context, services) =>
        {
            services.AddQuartz(configurator =>
            {
                configurator.AddJob<ProduceMessageToUpdateProductsCronJob>(context.Configuration);
            });
            services.AddQuartzHostedService(static opt => { opt.WaitForJobsToComplete = true; });
        });
}