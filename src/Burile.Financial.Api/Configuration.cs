using Burile.Financial.Api.Features.RetrieveEtfs;
using Burile.Financial.Infrastructure.Data.Contexts;
using Burile.Financial.Infrastructure.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Burile.Financial.Api;

public static class Configuration
{
    public static IWebHostBuilder BaseApiConfiguration(this IWebHostBuilder builder)
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
               .UseCors()
               .UseAuthorization()
               .UseEndpoints(static builder => builder.MapControllers());
        };

    private static Action<WebHostBuilderContext, IServiceCollection> ConfigureServices()
        => static (context, service)
            => service.AddEndpointsApiExplorer()
                      .AddSwaggerGen()
                      .AddMySqlDbContext<FinancialContext>(context.Configuration,
                                                           context.Configuration.GetConnectionString("Api"),
                                                           ServiceLifetime.Scoped,
                                                           typeof(Program).Assembly, typeof(FinancialContext).Assembly)
                      .AddHttpClient()
                      .AddMediatR(static configuration =>
                                      configuration.RegisterServicesFromAssemblyContaining<Program>())
                      .AddRetrieveEtfsServices()
                      .AddCors(static options => options.AddDefaultPolicy(static builder =>
                       {
                           builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                       }))
                      .AddFluentValidationAutoValidation()
                      .AddFluentValidationClientsideAdapters()
                      .AddValidatorsFromAssembly(typeof(Program).Assembly)
                      .AddControllers();
}