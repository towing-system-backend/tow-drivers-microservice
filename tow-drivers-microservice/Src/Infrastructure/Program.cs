using Application.Core;
using TowDriver.Extensions;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.ConfigureServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger();

app.MapGet("api/towdriver/health", () => Results.Ok("ok"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();