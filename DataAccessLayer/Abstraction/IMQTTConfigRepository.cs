using Models;
using Repository.Interface;

namespace DataAccessLayer.Abstraction
{
    public interface IMQTTConfigRepository : IGenericRepository<MQTTConfig>
    {
        Task<MQTTConfig> GetValueByGuidAsync(Guid guid);
    }
}
