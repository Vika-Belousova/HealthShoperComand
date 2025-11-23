var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration) // Чтение конфигурации из appsettings.json
        .Enrich.FromLogContext() // Добавление контекста в логи
        .WriteTo.Console(new JsonFormatter())
        .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341"); // Вывод в консоль
});
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
services.AddRouting(p => p.LowercaseUrls = true);
var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();
services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    {
        c.CustomSchemaIds(x => x.FullName);
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                []
            }
        });
    }
});
services.AddBllServiceCollection(configuration);
services.AddScoped<HostMiddleware>();

if (!args.Contains("--applicationName"))
{
    await using var scope = services.BuildServiceProvider().CreateAsyncScope();

    var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeed>();
    await dataSeed.Record();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseMiddleware<HostMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Console.WriteLine($"Running {DateTime.UtcNow}");
    await app.RunAsync();
}
catch (Exception exception)
{
    Console.WriteLine(exception);
    throw;
}

namespace HealthShoper
{
    public partial class Program
    {
    }
}