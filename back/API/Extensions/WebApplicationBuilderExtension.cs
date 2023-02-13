using System;
using System.IO;
using System.Text.RegularExpressions;
using API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace API.Extensions;

internal static class WebApplicationBuilderExtension
{
    internal static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        var logging = builder.Logging;
        logging.ClearProviders();

        var env = builder.Environment.EnvironmentName;
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env}.json", true)
            .Build();
        Console.WriteLine($"{nameof(env)} = '{env}'");
        
        var connectionString = Regex.Replace(configuration.GetConnectionString("Default"), 
            @"Database=(\w+)", @"Database=$1_log"); 
        var columnOptions = new ColumnOptions
        {
            TimeStamp = { ConvertToUtc = true, ColumnName = "CreatedTimeUtc", }
        };
        columnOptions.Store.Remove(StandardColumn.MessageTemplate);
        columnOptions.Store.Remove(StandardColumn.Properties);
        logging.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogEventLevel.Information)
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName   = "Logs",
                        AutoCreateSqlTable = true,
                },
                columnOptions: columnOptions)
            .CreateLogger());
    }

    internal static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.RegisterCors(configuration);
        services.AddControllers(x => x.Filters.Add<NoContentFilter>());
        services.AddMvc();
        services.AddHttpContextAccessor();

        services.RegisterIOptions(configuration);
        services.RegisterConnectionString(configuration);
        services.RegisterJwtAuthorization(configuration);

        services.RegisterServices();

        services.RegisterSwagger();
        services.RegisterServiceUri(configuration);
    }

    internal static WebApplication Configure(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var env = builder.Environment;
        var configuration = builder.Configuration;

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseMiddleware<ExceptionAndLoggerMiddleware>();

        app.UseHttpsRedirection();
        app.RegisterVirtualDir(configuration);

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.RegisterSwaggerUI();
        app.UseEndpoints(x => { x.MapControllers(); });

        return app;
    }
}
