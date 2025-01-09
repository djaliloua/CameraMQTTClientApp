using MQTTnet;
using System.Buffers;
using ViewModelLayer;

namespace MqttClientService
{
    public class MqttData
    {
        public string P_TopicName { get; set; }
        public string S_TopicName { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Message { get; set; }
        public CredentialViewModel Credential { get; set; }
        public MqttData(CameraViewModel cm)
        {
            S_TopicName = cm.TopicName;
            HostName = cm.HostName;
            Port = int.Parse(cm.Port);
            Credential = cm.Credential;
        }
        public MqttData()
        {
            
        }
    }
    public interface IMqttService
    {
        Task ConnectAsync(MqttData vm);
        Task DisconnectAsync();
        Task<bool> Publish(MqttData vm);
        bool IsConnected();
        event Action<byte[]> OnImageReceived;
    }
    public class MqttService : IMqttService
    {
        private readonly IMqttClient _mqttClient;
        public event Action<byte[]> OnImageReceived;

        public MqttService()
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
        }

        public async Task ConnectAsync(MqttData vm)
        {
            MqttClientOptions options;
            if(vm.Credential.IsValidate())
            {
                options = new MqttClientOptionsBuilder()
                    .WithTcpServer(vm.HostName, vm.Port)
                    .WithClientId("djalilou")
                    .WithCredentials(vm.Credential.UserName, vm.Credential.Password)
                    .Build();
            }
            else
            {
                options = new MqttClientOptionsBuilder()
                    .WithTcpServer(vm.HostName, vm.Port)
                    .Build();
            }
            

            _mqttClient.ApplicationMessageReceivedAsync += mqttClient_ApplicationMessageReceivedAsync;

            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(options);
            }

            await _mqttClient.SubscribeAsync(vm.S_TopicName);
        }

        private async Task mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            OnImageReceived?.Invoke(arg.ApplicationMessage.Payload.ToArray());
            await Task.CompletedTask;
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task<bool> Publish(MqttData vm)
        {
            if (!_mqttClient.IsConnected)
            {
                return false;
            }
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(vm.P_TopicName)
                .WithPayload(vm.Message)
                .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            return true;
        }

        public bool IsConnected()
        {
            return _mqttClient.IsConnected;
        }
    }
}
