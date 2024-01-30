using Burile.Financial.Api.Client.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Burile.Financial.UI.Client
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddFinancialApiClient("https://localhost:7052");

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}