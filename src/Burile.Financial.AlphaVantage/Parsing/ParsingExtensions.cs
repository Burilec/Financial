using System.Globalization;

namespace Burile.Financial.AlphaVantage.Parsing
{
    internal static class ParsingExtensions
    {
        internal static decimal ParseToDecimal(this string stringToParse) =>
            decimal.Parse(stringToParse, CultureInfo.InvariantCulture);

        internal static DateTime ParseToDateTime(this string stringToParse) =>
            DateTime.Parse(stringToParse, CultureInfo.InvariantCulture);

        internal static long ParseToLong(this string stringToParse) =>
            long.Parse(stringToParse, CultureInfo.InvariantCulture);
    }
}