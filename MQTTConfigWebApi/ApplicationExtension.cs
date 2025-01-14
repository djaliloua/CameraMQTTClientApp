using DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace MQTTConfigWebApi
{
    public static class ApplicationExtension
    {
        public static WebApplication RunMigrations(this WebApplication app, ILogger<Program> logger)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<MQTTConfigContext>();
            logger.LogInformation("abdou djalilou ali");
            logger.LogInformation(context.Database.GetConnectionString());
            context.Database.Migrate();
            return app;
        }
    }
}
