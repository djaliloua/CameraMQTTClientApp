using DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace MQTTConfigWebApi
{
    public static class ApplicationExtension
    {
        public static WebApplication RunMigrations(this WebApplication app, ILogger<Program> logger)
        {
            var context = new MQTTConfigContext();
            logger.LogInformation("abdou djalilou ali");
            logger.LogInformation(context.Database.GetConnectionString());
            context.Database.Migrate();
            return app;
        }
    }
}
