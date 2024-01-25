using System.Text.Json;

namespace Burile.Financial.AlphaVantage.Parsing
{
    internal static class MetaDataExtractor
    {
        private const string MetaDataToken = "Meta Data";

        internal static Dictionary<string, string> ExtractMetaData(this JsonDocument jsonDocument)
        {
            var result = new Dictionary<string, string>();

            var jsonProperties = jsonDocument.RootElement.EnumerateObject();
            if (jsonProperties.Any(static property => property.Name.Contains(MetaDataToken)) == false) return result;

            var metaDataProperty =
                jsonProperties.FirstOrDefault(static property => property.Name.Contains(MetaDataToken));

            if (metaDataProperty.Name.Contains(MetaDataToken) == false) return result;

            foreach (var metaData in metaDataProperty.Value.EnumerateObject())
                result.Add(metaData.Name, metaData.Value.ToString());

            return result;
        }
    }
}