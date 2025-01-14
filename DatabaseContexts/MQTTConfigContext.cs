using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseContexts
{
    public class MQTTConfigContext : DbContext
    {
        public DbSet<MQTTConfig> MQTTConfigs { get; set; }
        //dotnet ef database update -c MQTTConfigContext -s ../MQTTConfigWebApi
        //dotnet ef migrations add InitialCreate -c MQTTConfigContext -s ../MQTTConfigWebApi
        public MQTTConfigContext(DbContextOptions<MQTTConfigContext> options):base(options)
        {
            
        }
        
    }
}
