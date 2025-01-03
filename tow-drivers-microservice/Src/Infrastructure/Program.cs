using Application.Core;
using DotNetEnv;
using MassTransit;
using TowDrivers.Domain;
using TowDrivers.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddSingleton<MongoEventStore>();
builder.Services.AddScoped<IEventStore, MongoEventStore>();
builder.Services.AddScoped<IdService<string>, GuidGenerator>();
builder.Services.AddScoped<ITowDriverRepository, MongoTowDriverRepository>();
builder.Services.AddScoped<IMessageBrokerService, RabbitMQService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TowDriver API", Version = "v1" });
});


builder.Services.AddMassTransit(busConfigurator =>
{
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

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TowDriver v1");
    c.RoutePrefix = string.Empty;
});

app.MapGet("api/towdriver/health", () => Results.Ok("ok"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();