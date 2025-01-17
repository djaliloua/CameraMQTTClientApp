using DataAccessLayer.Abstraction;
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

        public async Task<MQTTConfig> GetValueByGuidAsync(Guid guid)
        {
            return await _table.FirstOrDefaultAsync(x => x.CameraId == guid);
        }

        
    }
}
