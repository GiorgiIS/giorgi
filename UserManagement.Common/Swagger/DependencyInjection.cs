using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace UserManagement.Common.Swagger
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            SwaggerOptions options;
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<SwaggerOptions>(configuration.GetSection("swagger"));
                options = configuration.GetSection("swagger").Get<SwaggerOptions>();
            }

            if (!options.Enabled)
            {
                return services;
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
              
                c.EnableAnnotations();
            });


            return services;
        }


        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<IConfiguration>()
                ?.GetSection("swagger").Get<SwaggerOptions>();
            if (!options.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");
  

            return builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                c.RoutePrefix = routePrefix;
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                c.DocExpansion(DocExpansion.None);
            });

           
        }
    }
}
