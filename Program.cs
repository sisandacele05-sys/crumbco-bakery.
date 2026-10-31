using CrumbCo.Models;
using CrumbCo.Services;

var builder = WebApplication.CreateBuilder(args);

// Cloud hosting supplies PORT. Do not override an explicit URL from Visual Studio,
// ASPNETCORE_URLS, or the command line.
var environmentUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
var configuredUrls = builder.Configuration["urls"];

if (string.IsNullOrWhiteSpace(environmentUrls) && string.IsNullOrWhiteSpace(configuredUrls))
{
    var configuredPort = Environment.GetEnvironmentVariable("PORT");
    var port = int.TryParse(configuredPort, out var parsedPort) && parsedPort is > 0 and <= 65535
        ? parsedPort
        : 3000;

    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddControllersWithViews();
builder.Services.Configure<BakerySettings>(builder.Configuration.GetSection("Bakery"));
builder.Services.AddSingleton<ProductCatalog>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/not-found");

app.MapGet("/_app/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();

app.Run();
