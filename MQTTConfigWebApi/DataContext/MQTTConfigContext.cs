using DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models;

namespace MQTTConfigWebApi.DataContext
{
    public class MQTTConfigContext : DbContext
    {
        public DbSet<MQTTConfig> MQTTConfigs { get; set; }
        public MQTTConfigContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<MQTTConfigContext>()
                .Build();
            optionsBuilder
                .UseSqlite(Configuration["connectionString"]);
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
