using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models;

namespace DatabaseContexts
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        private readonly string DatabasePurchase;
        public RepositoryContext()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            DatabasePurchase = Path.Combine(appDataPath, "CameraSettings2.db3");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePurchase}");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
