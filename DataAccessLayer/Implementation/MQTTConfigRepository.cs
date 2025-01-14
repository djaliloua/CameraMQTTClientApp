using DataAccessLayer.Abstraction;
using DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Implementation;

namespace DataAccessLayer.Implementation
{
    public class MQTTConfigRepository : GenericRepository<MQTTConfig>, IMQTTConfigRepository
    {
        public MQTTConfigRepository(DbContext dbContext) : base(dbContext)
        {

        }
        public MQTTConfigRepository()
        {
            //_dbContext = new MQTTConfigContext();
            //_table = _dbContext.Set<MQTTConfig>();
            //_dbContext.Database.EnsureCreated();
        }

        public async Task<MQTTConfig> GetValueByGuidAsync(Guid guid)
        {
            return await _table.FirstOrDefaultAsync(x => x.CameraId == guid);
        }

        
    }
}
