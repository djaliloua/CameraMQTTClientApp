using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DatabaseContexts
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        private readonly string DatabasePurchase;
        public RepositoryContext()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (OperatingSystem.IsWindows())
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<RepositoryContext>()
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
            optionsBuilder
                .UseSqlite($"Data Source={DatabasePurchase}");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
