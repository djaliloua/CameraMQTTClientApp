using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Implementation;
using Repository.Interface;

namespace MQTTConfigWebApi.Repo
{
    public interface IMQTTConfigRepository:IGenericRepository<MQTTConfig>
    {
    }

    public class MQTTConfigRepository: GenericRepository<MQTTConfig>, IMQTTConfigRepository
    {
        public MQTTConfigRepository(DbContext dbContext):base(dbContext)
        {
            
        }
        public MQTTConfigRepository()
        {
            
        }
    }
}
