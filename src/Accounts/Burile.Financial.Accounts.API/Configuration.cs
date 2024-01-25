namespace Burile.Financial.Accounts.Api;

internal static class Configuration
{
    internal static IWebHostBuilder BaseApiConfiguration(this IWebHostBuilder builder)
        => builder
          .ConfigureServices(ConfigureServices())
          .Configure(ConfigureApp());

    private static Action<WebHostBuilderContext, IApplicationBuilder> ConfigureApp()
        => static (context, app) =>
        {
            // Configure the HTTP request pipeline.
            if (context.HostingEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting()
               .UseHttpsRedirection()
               .UseAuthorization()
               .UseEndpoints(static builder => builder.MapControllers());
        };

    private static Action<WebHostBuilderContext, IServiceCollection> ConfigureServices()
        => static (_, service)
            => service.AddEndpointsApiExplorer()
                      .AddSwaggerGen()
                      .AddHttpClient()
                      .AddControllers();
}