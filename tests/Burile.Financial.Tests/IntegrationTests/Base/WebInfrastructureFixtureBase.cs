using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Burile.Financial.Tests.IntegrationTests.Base;

public abstract class WebInfrastructureFixtureBase
{
    protected WebInfrastructureFixtureBase()
    {
        HostBuilder = SetupHostBuilder();
        Host = HostBuilder.Build();
        Host.StartAsync();
        TestServer = Host.GetTestServer();
        HttpClient = TestServer.CreateClient();
        ServiceProvider = Host.Services;
        SetupInitialize();
    }

    private IHost Host { get; }
    private TestServer TestServer { get; }
    internal HttpClient HttpClient { get; private set; }
    private IHostBuilder HostBuilder { get; }
    public IServiceProvider ServiceProvider { get; private set; }

    private IHostBuilder SetupHostBuilder()
        => CreatedHostBuilder();

    private void SetupInitialize()
        => Initialize();

    protected abstract IHostBuilder CreatedHostBuilder();

    protected abstract void Initialize();
}