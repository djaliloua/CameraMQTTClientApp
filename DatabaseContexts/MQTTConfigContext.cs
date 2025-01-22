using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DatabaseContexts
{
    public class MQTTConfigContext : DbContext
    {
        public DbSet<MQTTConfig> MQTTConfigs { get; set; }
        //dotnet ef database update -c MQTTConfigContext -s ../MQTTConfigWebApi
        //dotnet ef migrations add InitialCreate -c MQTTConfigContext -s ../MQTTConfigWebApi
        private readonly string DatabasePurchase;
        public MQTTConfigContext(DbContextOptions<MQTTConfigContext> options):base(options)
        {
            
        }
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
                DatabasePurchase = Path.Combine(appDataPath, "camerasettings.db3");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePurchase}");
        }

    }
}
