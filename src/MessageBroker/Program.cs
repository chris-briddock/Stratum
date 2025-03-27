
using System.Reflection;
using Api.Middlware;
using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Application.Services;
using ChristopherBriddock.AspNetCore.Extensions;
using ChristopherBriddock.AspNetCore.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;
using Scalar.AspNetCore;

namespace MessageBroker;

/// <summary>
/// The entry point for the Web Application.
/// </summary>
public sealed class Program
{
    /// <summary>
    /// The entry method for the web application.
    /// </summary>
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.AddKestrelConfiguration(7172);
        builder.ConfigureOpenTelemetry(ServiceNameDefaults.ServiceName);
        builder.Services.Configure<HostOptions>(options =>
        {
            options.ServicesStartConcurrently = true;
            options.ServicesStopConcurrently = true;
        });
        builder.Services.AddFeatureManagement();
        builder.Services.AddOpenApi();
        builder.Services.AddPersistence();
        builder.Services.AddStores();
        builder.Services.AddServices();
        builder.Services.AddDataProtection();
        builder.Services.AddInMemoryCache();
        builder.Services.AddDistributedCache(builder.Configuration);
        builder.Services.AddBearerAuthentication(builder.Configuration);
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDataProtection();
        builder.Services.AddControllers();
        builder.Services.AddMetrics();
        builder.Services.AddHybridCache(builder.Configuration);
        builder.Services.AddCustomSession();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddVersioning(1,0);
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHealthChecks();
        builder.Services.AddResponseCompression();
        builder.Services.AddSqlDatabaseHealthChecks(builder.Configuration.GetConnectionStringOrThrow("WriteConnection"));
        builder.Services.AddRedisHealthCheck(builder.Configuration);
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        app.UseSession();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<SessionMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHsts();
        app.MapControllers();
        app.UseCustomHealthCheckMapping();
        app.UseHttpsRedirection();
        await app.UseSeedDataAsync();
        await app.RunAsync();
    }
}
