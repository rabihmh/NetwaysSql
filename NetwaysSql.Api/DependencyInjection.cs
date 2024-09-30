namespace NetwaysSql.Api
{
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Models;
    using Netways.Dynamics.Common.Core;
    using Netways.Sql.Core;
    using NetwaysSql.Model;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            /// <summary>
            ///  Thic Code block is used to add all the fluentValidation validators from all the assemblies
            /// </summary>
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                services.AddValidatorsFromAssembly(assembly);
            }

            //Add our custom validation services, that works with fluentValidation and data annotation 
            services.AddValidationServices();

            //Stop the default model state validation (Data annotation)
            services.StopModelStateValidation();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type));

                c.DocumentFilter<ConsumerKeyBasedDocumentFilter>(controllerTypes);
            });
        }

        public static void AddWriteDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServices<WriteDbContext>(configuration.GetConnectionString("DefaultConnection") ?? "",
                options =>
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                },
                serviceLifetime: ServiceLifetime.Transient
            );
        }

        public static void AddReadDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServices<ReadDbContext>(configuration.GetConnectionString("DefaultConnection") ?? "",
                options =>
                {
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                },
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);//we should remove this line if we will use database transactions
                },
                serviceLifetime: ServiceLifetime.Transient
            );

        }
    }
}

