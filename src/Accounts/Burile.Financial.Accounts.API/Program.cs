using Microsoft.AspNetCore;

namespace Burile.Financial.Accounts.Api;

internal sealed class Program
{
    public static async Task Main(string[] args)
        => await WebHost.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration(ConfigureAppConfiguration())
                        .BaseApiConfiguration()
                        .Build()
                        .RunAsync().ConfigureAwait(false);

    private static Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfiguration()
        => static (context, configuration)
            => configuration.SetBasePath(context.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                                         optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();
}