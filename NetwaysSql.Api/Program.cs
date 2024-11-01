using Netways.Dynamics.Common.Core;
using Netways.Sql.Core;
using NetwaysSql.Api;
using NetwaysSql.Core;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

builder.Services.AddApiVersioningService();

builder.Services.AddCommonServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClientService(useCircuitBreakerPolicy: false, useRetryPolicy: false);

builder.Services.AddCommonServices();

builder.Services.AddOnPremiseLoggerServices(builder.Configuration);

builder.Services.RegisterValidators();

builder.Services.AddSwaggerFilterServices();

builder.Services.AddResponseCompressionService();

builder.Services.AddWriteDbContext(builder.Configuration);

builder.Services.AddReadDbContext(builder.Configuration);

builder.Services.AddSqlHealthChecks(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");

builder.Services.AddTransient<ICategoryManager, CategoryManager>();

builder.Services.AddTransient<IProductManager, ProductManager>();

builder.Services.AddDapperServices("Server=RABIHMH-NET;Database=NetwaysSql;Integrated Security=True;TrustServerCertificate=true");

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.AddSwaggerUIVersions();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddLoggerApplications();

app.AddSwaggerFilterApplications();

app.MapHealthChecks();

app.EnableResponseCompression();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.Run();
