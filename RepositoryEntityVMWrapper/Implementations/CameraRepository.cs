using BaseViewModels;
using KiotaOpenAIClient;
using KiotaOpenAIClient.Client.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Repository;
using RepositoryEntityVMWrapper.Abstractions;

namespace RepositoryEntityVMWrapper.Implementations
{
    public class CameraRepositoryApi : ICameraRepo
    {
        private readonly IApiService _apiService;
        public CameraRepositoryApi(DbContext dbContext) : base(dbContext)
        {

        }
        public CameraRepositoryApi()
        {
            _apiService = new ApiService();
        }
        public override async Task<IList<MQTTConfigViewModel>> GetAllToViewModel()
        {
            var result = await _apiService.GetMQTTConfigsAsync();
            return result.ToVM<List<MQTTConfig>, List<MQTTConfigViewModel>>();
        }
        public override async Task<MQTTConfig> UpdateAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.FromVM<MQTTConfigViewModel, MQTTConfig>();
            var config = await _apiService.UpdateMQTTConfigAsync(entity.Id, obj);
            return config.Adapt<MQTTConfig>();
        }
        public override async Task<MQTTConfigViewModel> SaveAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.FromVM<MQTTConfigViewModel, MQTTConfig>();
            var config = await _apiService.CreateMQTTConfigAsync(obj);
            return config.ToVM<MQTTConfig, MQTTConfigViewModel>();
        }
        public override void Delete(object id)
        {
            _apiService.DeleteMQTTConfigAsync((int)id);
        }
    }
    public class CameraRepository : ICameraRepo
    {
        public CameraRepository(DbContext dbContext) : base(dbContext)
        {
            _table = dbContext.Set<MQTTConfig>();
            if (OperatingSystem.IsAndroid())
            {
                _dbContext.Database.EnsureCreated();
            }
        }
    }
}
