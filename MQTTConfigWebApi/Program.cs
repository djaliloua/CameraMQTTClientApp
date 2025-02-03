using DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MQTTConfigWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //builder.WebHost.UseUrls("http://0.0.0.0:5000");
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.AddServiceDefaults();
        builder.Services.AddTransient<MQTTConfigContext>();
        //builder.Services.AddDbContext<MQTTConfigContext>(option =>
        //{
        //    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));
        //});
        builder.Logging.AddConsole();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowSpecificOrigin",
        //        builder =>
        //        {
        //            builder.WithOrigins("http://0.0.0.0:5000") // Replace with your client URL
        //                   .AllowAnyHeader()
        //                   .AllowAnyMethod();
        //        });
        //});

        builder
            .Services
            .AddControllers();
            
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MQTT Configuration API",
                Description = "An ASP.NET Core Web API for managing Account items",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("https://example.com/license")
                }
            });
            //
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.DocumentTitle = "MQTT Config Swagger";
            });
            
        }
        app.MapDefaultEndpoints();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
        app.RunMigrations(logger);
        //app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy
        app.MapGet("/", () => "Hello World!");
        app.MapControllers();

        app.Run();
    }
}
