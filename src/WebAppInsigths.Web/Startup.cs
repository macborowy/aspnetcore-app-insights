using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (context.Request.Query.TryGetValue("i", out StringValues value) && int.TryParse(value, out int i) && i % 10 == 0)
                {
                    throw new Exception("FAIL!");
                }

                await context.Response.WriteAsync($"Hello from {context.Request.Method} {context.Request.Path}!");
            });
        }
    }
}
