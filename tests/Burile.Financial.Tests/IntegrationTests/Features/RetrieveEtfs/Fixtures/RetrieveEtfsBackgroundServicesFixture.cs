using Burile.Financial.Api;
using Burile.Financial.Tests.IntegrationTests.Base;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;
using static Microsoft.Extensions.Hosting.Host;

namespace Burile.Financial.Tests.IntegrationTests.Features.RetrieveEtfs.Fixtures;

[CollectionDefinition("RetrieveEtfsBackgroundServicesTest")]
public class BackgroundServicesCollection : ICollectionFixture<RetrieveEtfsBackgroundServicesFixture>;

public sealed class RetrieveEtfsBackgroundServicesFixture : WebInfrastructureFixtureBase
{
    private const string HostEnvironment = "Local";

    protected override IHostBuilder CreatedHostBuilder()
    {
        var hostBuilder = CreateDefaultBuilder(Array.Empty<string>())
                         .UseEnvironment(HostEnvironment)
                         .ConfigureHostConfiguration(ConfigureHostConfiguration())
                         .ConfigureWebHostDefaults(static builder => builder.UseTestServer().BaseApiConfiguration());

        return hostBuilder;
    }

    private static Action<IConfigurationBuilder> ConfigureHostConfiguration()
        => static builder => builder.SetBasePath(Environment.CurrentDirectory)
                                    .AddEnvironmentVariables();

    protected override void Initialize()
    {
    }
}