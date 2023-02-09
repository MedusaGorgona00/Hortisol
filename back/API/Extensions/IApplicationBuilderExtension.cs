using System.IO;
using Common.Extensions;
using Common.Helpers;
using DAL.EF;
using DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace API.Extensions;

internal static class IApplicationBuilderExtension
{
    internal static void RegisterVirtualDir(this IApplicationBuilder app, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(SettingsDto.VirtualDir)).Get<SettingsDto.VirtualDir>();
        var dir = Directory.GetCurrentDirectory().Combine(settings.BaseDir);
        dir.CreateDirectoryIfNotExist();
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(dir),
            RequestPath = new PathString(settings.BaseSuffixUri),
        });
        AppConstants.BaseDir = settings.BaseDir;
    }

    internal static void RegisterSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "hortisol api v1");
            x.RoutePrefix = string.Empty;
            x.DefaultModelExpandDepth(3);
            x.DefaultModelRendering(ModelRendering.Example);
            x.DefaultModelsExpandDepth(-1);
            x.DisplayOperationId();
            x.DisplayRequestDuration();
            x.DocExpansion(DocExpansion.None);
            x.EnableDeepLinking();
            x.EnableFilter();
            x.ShowExtensions();
        });
    }

    internal static void AutoMigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }
}
