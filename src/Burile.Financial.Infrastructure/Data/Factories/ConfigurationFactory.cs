using Burile.Financial.Infrastructure.Hosting;
using Microsoft.Extensions.Configuration;

namespace Burile.Financial.Infrastructure.Data.Factories;

internal static class ConfigurationFactory
{
    internal static IConfiguration CreateConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appsettings.json");

        var aspNetCoreEnvironment = HostingEnvironmentVariables.GetAspNetCoreEnvironment();
        var dotnetCoreEnvironment = HostingEnvironmentVariables.GetDotNetEnvironment();

        if (!string.IsNullOrWhiteSpace(aspNetCoreEnvironment))
        {
            configurationBuilder.AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json", true, true);
        }

        if (!string.IsNullOrWhiteSpace(dotnetCoreEnvironment))
        {
            configurationBuilder.AddJsonFile($"appsettings.{dotnetCoreEnvironment}.json", true, true);
        }

        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}