using Burile.Financial.AlphaVantage.Enums;

namespace Burile.Financial.AlphaVantage.Extensions;

internal static class IntervalExtensions
{
    internal static ApiFunction ConvertToApiFunction(this Interval interval, bool isAdjusted)
    {
        return (timeSeriesType: interval, isAdjusted) switch
        {
            (Interval.DAILY, false) => ApiFunction.TIME_SERIES_DAILY,
            (Interval.DAILY, true) => ApiFunction.TIME_SERIES_DAILY_ADJUSTED,

            (Interval.WEEKLY, false) => ApiFunction.TIME_SERIES_WEEKLY,
            (Interval.WEEKLY, true) => ApiFunction.TIME_SERIES_WEEKLY_ADJUSTED,

            (Interval.MONTHLY, false) => ApiFunction.TIME_SERIES_MONTHLY,
            (Interval.MONTHLY, true) => ApiFunction.TIME_SERIES_MONTHLY_ADJUSTED,

            _ => ApiFunction.TIME_SERIES_INTRADAY
        };
    }

    internal static string ConvertToQueryString(this Interval interval)
    {
        return interval switch
        {
            Interval.MIN1 => "1min",
            Interval.MIN5 => "5min",
            Interval.MIN15 => "15min",
            Interval.MIN30 => "30min",
            Interval.MIN60 => "60min",
            Interval.DAILY => "daily",
            Interval.WEEKLY => "weekly",
            Interval.MONTHLY => "monthly",
            _ => string.Empty
        };
    }
}