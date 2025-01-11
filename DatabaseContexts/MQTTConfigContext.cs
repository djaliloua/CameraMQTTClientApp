using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DatabaseContexts
{
    public class MQTTConfigContext : DbContext
    {
        private readonly string DatabasePurchase;
        public DbSet<MQTTConfig> MQTTConfigs { get; set; }
        public MQTTConfigContext()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (OperatingSystem.IsWindows())
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<MQTTConfigContext>()
                .Build();
                DatabasePurchase = Configuration["local_db_folder"];
            }
            else
            {
                DatabasePurchase = Path.Combine(appDataPath, "mqttconfigfile.db3");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source={DatabasePurchase}");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
