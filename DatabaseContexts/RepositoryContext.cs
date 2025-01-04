using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Models;

namespace DatabaseContexts
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Camera> Cameras { get; set; }
        private string DatabasePurchase;
        public RepositoryContext()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            DatabasePurchase = Path.Combine(appDataPath, "my_database.db3");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePurchase}");
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
        }
    }
}
