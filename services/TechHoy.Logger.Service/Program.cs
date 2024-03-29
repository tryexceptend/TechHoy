using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Authentication;
using TechHoy.Logger.LogMessagesController;
using TechHoy.Logger.Service.HealthController;

var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddConfiguration(configuration);

//builder.Services.ConfigureHealthCheckes();

// Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Limits.MaxRequestBodySize = 4194304L;
    options.ConfigureHttpsDefaults(delegate (HttpsConnectionAdapterOptions httpsOptions)
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
    options.ListenAnyIP(Convert.ToInt32(builder.Configuration["hosting:httpPort"]), delegate (ListenOptions httpsOptions)
    {
        httpsOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

// Контроллеры и сериализация
builder.Services.AddControllers();
builder.Services.AddMvcCore().AddApiExplorer().SetCompatibilityVersion(CompatibilityVersion.Latest);
builder.Services.AddMvc().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddLoggerDependencies(builder.Configuration);


builder.Services.AddSwaggerGen(swagger =>
{
    var title = Assembly.GetExecutingAssembly().FullName;
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);
    swagger.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddHealthChecks().AddDataBaseHealthCheck();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

app.MapHealthChecks("/health");
app.UseCryptoHealthChecks("/healthJson");
app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.Run();
