using BaseViewModels;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OpenAIClient;
using Repository;
using RepositoryEntityVmAdpter.Abstractions;
using Models;
using System.Net.Security;

namespace RepositoryEntityVmAdpter.Implementations
{
    public class CameraRepositoryApi : ICameraRepoApi
    {
        private readonly IClient _apiService;
        public CameraRepositoryApi(DbContext dbContext) : base(dbContext)
        {

        }
        public CameraRepositoryApi()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) =>
                {
                    if (policyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                    {
                        // Log the issue (optional)
                        //Console.WriteLine("Ignoring RemoteCertificateNameMismatch");
                    }

                    // Ignore all certificate errors in development
                    return true;
                }
            };
            _apiService = new Client("https://192.168.1.131:5001", new HttpClient(handler));
        }
        public override async Task<IList<MQTTConfigViewModel>> GetAllToViewModel()
        {
            var result = await _apiService.MqttConfigAllAsync();
            List <MQTTConfigViewModel> data = new List<MQTTConfigViewModel>();
            foreach(var item in result)
            {
                data.Add(item.Adapt<MQTTConfigViewModel>());
            }
            return data;
        }
        public override async Task<MQTTConfig> UpdateAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.Adapt<MQTTConfig>();
            var config = await _apiService.MqttConfigPUTAsync(entity.Id, obj);
            return config.Adapt<MQTTConfig>();
        }
        public override async Task<MQTTConfigViewModel> SaveAsync(MQTTConfigViewModel entity)
        {
            var obj = entity.FromVM<MQTTConfigViewModel, MQTTConfig>();
            var config = await _apiService.MqttConfigPOSTAsync(obj);
            return config.ToVM<MQTTConfig, MQTTConfigViewModel>();
        }
        public override void Delete(object id)
        {
            _apiService.MqttConfigDELETEAsync((int)id);
        }
    }
    public class CameraRepository : ICameraRepo
    {
        public CameraRepository(DbContext dbContext) : base(dbContext)
        {
            _table = dbContext.Set<Models.MQTTConfig>();
            dbContext.Database.Migrate();
        }
    }
}
