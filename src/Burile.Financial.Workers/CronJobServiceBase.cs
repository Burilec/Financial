using System.Diagnostics;
using Quartz;

namespace Burile.Financial.Workers;

public abstract class CronJobServiceBase(IConfiguration configuration) : IHostedService, IDisposable
{
    private CronExpression? _cronExpression;

    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        var configCronExpression = configuration.GetValue<string>($"{GetType().Name}:CronExpression");

        if (string.IsNullOrWhiteSpace(configCronExpression))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(configCronExpression));

        _cronExpression = new CronExpression(configCronExpression);

        await ScheduleJobAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
    }

    private async Task ScheduleJobAsync(CancellationToken cancellationToken)
    {
        Debug.Assert(_cronExpression != null, nameof(_cronExpression) + " != null");
        var timeAfter = _cronExpression.GetTimeAfter(DateTimeOffset.Now);

        if (timeAfter != null)
        {
            var delta = timeAfter.Value - DateTimeOffset.Now;
            if (delta.TotalMilliseconds <= 0)
            {
                await ExecuteAndScheduleJobAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }
            else
            {
            }
        }
    }

    private async Task ExecuteAndScheduleJobAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}