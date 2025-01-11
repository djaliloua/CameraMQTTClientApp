using ApiCallServices;
using Models;
using System.Configuration;


IApiService apiService = new ApiService(new HttpClient(), ConfigurationManager.AppSettings["ApiUrl"]);
//var mqttConfig = await apiService.GetByIdAsync<MQTTConfig>(1);
//Console.WriteLine(mqttConfig.HostName);
//Console.WriteLine(mqttConfig.Password);
var allConfigs = await apiService.GetAllAsync<List<MQTTConfig>>();
Console.WriteLine(allConfigs.Count);


