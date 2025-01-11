using DatabaseContexts;
using KiotaOpenAIClient;
using Mapster;
using Models;
using Repository;
using Repository.Implementation;

namespace ViewModelLayer.DataAccessLayer
{
    public abstract class ICameraRepo: GenericRepositoryViewModel<MQTTConfig, MQTTConfigViewModel>
    {

    }
    public class CameraRepositoryApi : ICameraRepo
    {
        private readonly IApiService _apiService;
        public CameraRepositoryApi()
        {
            _apiService = new ApiService();
        }
        public override async Task<IList<MQTTConfigViewModel>> GetAllToViewModel()
        {
            var result = await _apiService.GetMQTTConfigsAsync();
            return result.ToVM<List<KiotaOpenAIClient.Client.Models.MQTTConfig>, List< MQTTConfigViewModel>>();
        }
        public override async Task<MQTTConfig> UpdateAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.FromVM<MQTTConfigViewModel, KiotaOpenAIClient.Client.Models.MQTTConfig>();
            var config = await _apiService.UpdateMQTTConfigAsync(entity.Id, obj);
            return config.Adapt<MQTTConfig>();
        }
        public override async Task<MQTTConfigViewModel> SaveAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.FromVM<MQTTConfigViewModel, KiotaOpenAIClient.Client.Models.MQTTConfig>();
            var config = await _apiService.CreateMQTTConfigAsync(obj);
            return config.ToVM<KiotaOpenAIClient.Client.Models.MQTTConfig, MQTTConfigViewModel>();
        }
        public override void Delete(object id)
        {
            _apiService.DeleteMQTTConfigAsync((int)id);
        }
    }
    public class CameraRepository : ICameraRepo
    {
        public CameraRepository()
        {
            _dbContext = new MQTTConfigContext();
            _table = _dbContext.Set<MQTTConfig>();
            if (OperatingSystem.IsAndroid())
            {
                _dbContext.Database.EnsureCreated();
            }
        }
    }
}
