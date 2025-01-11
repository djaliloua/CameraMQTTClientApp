// API requires no authentication, so use the anonymous
// authentication provider
using MQTTConfigApiConsume;

IApiService apiService = new  ApiService();
var config = await apiService.GetMQTTConfigByGuidAsync(Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"));
Console.WriteLine(config.HostName);