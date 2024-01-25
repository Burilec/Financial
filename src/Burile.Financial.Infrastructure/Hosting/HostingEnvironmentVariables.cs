namespace Burile.Financial.Infrastructure.Hosting;

internal static class HostingEnvironmentVariables
{
    private const string DotNetEnvironment = "DOTNET_ENVIRONMENT";
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    internal static string? GetDotNetEnvironment()
        => Environment.GetEnvironmentVariable(DotNetEnvironment);

    internal static string? GetAspNetCoreEnvironment()
        => Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
}