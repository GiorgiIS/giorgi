using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UserManagement.Common.EfCore;
using UserManagement.Common.EfCore.Contracts;

namespace SubContractors.Infrastructure.Persistence
{
    public static class DependencyInjection
    {

        public static void AddEfCore<TContext>(this IServiceCollection services, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext, new()
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var efCore = configuration.GetSection("efCore")
                                      .Get<EfCoreOptions>();
            services.Configure<EfCoreOptions>(configuration.GetSection("efCore"));

            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddDbContext<TContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                if (efCore.Environment is "development" or "production")
                {
                    options.UseSqlServer(efCore.ConnectionString);
                }
                else
                {
                    var connectionString = $"DataSource={Guid.NewGuid()};mode=memory;cache=shared";


                    var keepAliveConnection = new SqliteConnection(connectionString);
                    keepAliveConnection.Open();
                    options.UseSqlite(connectionString);
                }
            }, contextLifetime, optionsLifetime);
            AddRepositories(services, typeof(TContext));
        }
        
        private static void AddRepositories(IServiceCollection serviceCollection, Type dbContextType)
        {
            var repoInterfaceType = typeof(ISqlRepository<,>);
            var repoImplementationType = typeof(SqlRepository<,,>);
            foreach (var entityType in GetEntityTypes(dbContextType))
            {
                var identifierType = entityType.GetProperty("Id")
                                              ?.PropertyType;
                var genericRepoInterfaceType = repoInterfaceType.MakeGenericType(entityType, identifierType);
                if (serviceCollection.Any(x => x.ServiceType == genericRepoInterfaceType))
                {
                    continue;
                }

                var genericRepoImplementationType = repoImplementationType.MakeGenericType(entityType, dbContextType, identifierType);
                serviceCollection.AddScoped(genericRepoInterfaceType, genericRepoImplementationType);
            }
        }

        public static IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(x => x.PropertyType.IsAssignableToGenericType(typeof(DbSet<>)))
                                .Select(x => x.PropertyType.GenericTypeArguments[0]);
        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType.GetTypeInfo()
                         .IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo()
                                 .IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.GetTypeInfo()
                         .BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenType.GetTypeInfo()
                                                      .BaseType, genericType);
        }
    }
}