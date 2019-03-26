using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebAppInsigths.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddAzureWebAppDiagnostics();
            });

            //var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
            //aiOptions.EnableAdaptiveSampling = false;
            //services.AddApplicationInsightsTelemetry(aiOptions);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            //configuration.TelemetryChannel.DeveloperMode = true;

            // var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();

            // var builder = configuration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;
            // version 2.5.0-beta2 and above should use the following line instead of above. (https://github.com/Microsoft/ApplicationInsights-aspnetcore/blob/develop/CHANGELOG.md#version-250-beta2)
            // var builder = configuration.DefaultTelemetrySink.TelemetryProcessorChainBuilder;

            // Using adaptive sampling
            // builder.UseAdaptiveSampling(maxTelemetryItemsPerSecond: 5);
            

            // Alternately, the following configures adaptive sampling with 5 items per second, and also excludes DependencyTelemetry from being subject to sampling.
            // builder.UseAdaptiveSampling(maxTelemetryItemsPerSecond:5, excludedTypes: "Dependency");

            //builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello from {context.Request.Method} {context.Request.Path}!");
            });
        }
    }
}
