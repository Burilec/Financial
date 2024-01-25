using Microsoft.Extensions.Configuration;
using Quartz;

namespace Burile.Financial.Infrastructure.Jobs;

public static class SetupJobs
{
    public static IServiceCollectionQuartzConfigurator AddJob<T>(this IServiceCollectionQuartzConfigurator service,
                                                                 IConfiguration configuration,
                                                                 string? name = null,
                                                                 string? cronExpression = null) where T : IJob
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(configuration);

        if (string.IsNullOrWhiteSpace(name))
        {
            name = typeof(T).Name;
        }

        if (!string.IsNullOrWhiteSpace(cronExpression))
            return ServiceCollectionQuartzConfigurator<T>(service, name, cronExpression);

        var configurationCronExpression = configuration.GetValue<string>($"{name}:CronExpression");

        if (string.IsNullOrWhiteSpace(configurationCronExpression))
        {
            throw new Exception($"Value cannot be null or white space {nameof(configurationCronExpression)}");
        }

        cronExpression = configurationCronExpression;

        return ServiceCollectionQuartzConfigurator<T>(service, name, cronExpression);
    }

    private static IServiceCollectionQuartzConfigurator ServiceCollectionQuartzConfigurator<T>(
        IServiceCollectionQuartzConfigurator service, string name, string cronExpression) where T : IJob
    {
        return service.AddJob<T>(configurator => configurator.WithIdentity(name))
                      .AddTrigger(configurator => configurator.ForJob(name).WithCronSchedule(cronExpression));
    }
}