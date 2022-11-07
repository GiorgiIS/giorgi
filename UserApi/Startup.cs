using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using SubContractors.Infrastructure.Persistence;
using System.Reflection;
using UserManagement.Application.Handlers.User.Queries.GetUser;
using UserManagement.Common.FluentValidation;
using UserManagement.Common.Mediator;
using UserManagement.Common.Mvc;
using UserManagement.Common.Swagger;
using UserManagement.Infrastructure.Persistence.EfCore;

namespace UserManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Assembly.GetExecutingAssembly());
            services.AddSwaggerDocumentation();
            services.AddEfCore<UserManagementDbContext>();
            services.AddMediator(typeof(GetUserQuery).Assembly);
            services.AddFluentValidation(typeof(GetUserQuery).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appOptions = Configuration.GetSection("app").Get<AppOptions>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserApi v1"));
            }

            app.UseSwaggerDocs();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
