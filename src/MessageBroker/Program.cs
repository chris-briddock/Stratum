
using System.Reflection;
using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Application.Services;
using AspNetCore.Scalar;
using ChristopherBriddock.AspNetCore.Extensions;
using ChristopherBriddock.AspNetCore.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;

namespace MessageBroker;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureOpenTelemetry(ServiceNameDefaults.ServiceName);
        builder.Services.Configure<HostOptions>(options =>
        {
            options.ServicesStartConcurrently = true;
            options.ServicesStopConcurrently = true;
        });
        builder.Services.AddOpenApi();
        builder.Services.AddDataProtection();
        builder.Services.AddFeatureManagement();
        builder.Services.AddBearerAuthentication(builder.Configuration);
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDataProtection();
        builder.Services.AddControllers();
        builder.Services.AddMetrics();
        builder.Services.AddHybridCache();
        builder.Services.AddCustomSession();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddVersioning(1,0);
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddPersistence();
        builder.Services.AddMessageBrokerChannels();
        builder.Services.TryAddScoped<IPublisher, Publisher>();
        builder.Services.TryAddScoped<ISubscriber, Subscriber>();
        builder.Services.AddResponseCompression();
        builder.Services.AddSqlDatabaseHealthChecks(builder.Configuration.GetConnectionStringOrThrow("Default"));
        builder.Services.AddRedisHealthCheck(builder.Configuration);
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseScalar();
        }

        app.MapGet("/", () => "Hello, World!");

        app.UseHsts();
        app.MapControllers();
        app.UseCustomHealthCheckMapping();
        

        app.UseHttpsRedirection();
        await app.RunAsync();
    }
}
