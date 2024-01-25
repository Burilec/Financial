using Quartz;

namespace Burile.Financial.CronJobs.Features.ProduceMessageToUpdateProducts;

[DisallowConcurrentExecution]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ProduceMessageToUpdateProductsCronJob(ILogger<ProduceMessageToUpdateProductsCronJob> logger) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("{UtcNow}", DateTime.UtcNow);

        return Task.CompletedTask;
    }
}