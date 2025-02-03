using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using MQTTConfigApiConsume.Client;
using MQTTConfigApiConsume.Client.Models;

namespace MQTTConfigApiConsume
{
    public class ApiService: IApiService
    {
        private readonly HttpClientRequestAdapter _adapter;
        private readonly MQTTConfigClient _client;
        public ApiService()
        {
            var authProvider = new AnonymousAuthenticationProvider();
            _adapter = new HttpClientRequestAdapter(authProvider) { BaseUrl= "http://localhost:5094" };
            _client = new MQTTConfigClient(_adapter);
        }
        public ApiService(string baseUrl)
        {
            var authProvider = new AnonymousAuthenticationProvider();
            _adapter = new HttpClientRequestAdapter(authProvider) { BaseUrl = baseUrl };
            _client = new MQTTConfigClient(_adapter);
        }
        public async Task<MQTTConfig> CreateMQTTConfigAsync(MQTTConfig config)
        {
            return await _client.MqttConfig.PostAsync(config);
        }
        public Task DeleteMQTTConfigAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        public async Task<List<MQTTConfig>> GetMQTTConfigsAsync()
        {
            return await _client.MqttConfig.GetAsync();
        }
        public async Task<MQTTConfig> GetMQTTConfigAsync(int id)
        {
            return await _client.MqttConfig[id].GetAsync();
        }
        public async Task<MQTTConfig> GetMQTTConfigByGuidAsync(Guid cameraId)
        {
            return await _client.MqttConfig.App[cameraId].GetAsync();
        }
        public async Task<MQTTConfig> UpdateMQTTConfigAsync(int id, MQTTConfig config)
        {
            return await _client.MqttConfig[id].PutAsync(config);
        }
    }
   
    public interface IApiService
    {
        Task<List<MQTTConfig>> GetMQTTConfigsAsync();
        Task<MQTTConfig> GetMQTTConfigAsync(int id);
        Task<MQTTConfig> CreateMQTTConfigAsync(MQTTConfig config);
        Task<MQTTConfig> UpdateMQTTConfigAsync(int id, MQTTConfig config);
        Task DeleteMQTTConfigAsync(int id);
        Task<MQTTConfig> GetMQTTConfigByGuidAsync(Guid cameraId);
    }
}
