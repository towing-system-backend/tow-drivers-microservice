using Application.Core;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TowDriver.Domain;
using TowDriver.Infrastructure;

namespace TowDriver.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IdService<string>, GuidGenerator>();
            services.AddScoped<Logger, DotNetLogger>();
            services.AddScoped<IMessageBrokerService, RabbitMQService>();
            services.AddScoped<TowDriverController>();
            services.AddScoped<ISupplierCompanyRespository, MongoSupplierCompaniesRespository>();
            services.AddSingleton<MongoEventStore>();
            services.AddSingleton<IEventStore, MongoEventStore>();
            services.AddSingleton<IPerformanceLogsRepository, MongoPerformanceLogsRespository>();
            services.AddSingleton<ITowDriverRepository, MongoTowDriverRepository>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        RoleClaimType = ClaimTypes.Role,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!))
                    };
                });
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<CreateTowDriverConsumer>();
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI")!), h =>
                    {
                        h.Username(Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")!);
                        h.Password(Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")!);
                    });

                    configurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    configurator.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "TowDriver API", Version = "v1" });
            });
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TowDriver v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
