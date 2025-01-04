using MQTTnet;
using System.Buffers;

namespace MauiCamMqttClient
{
    public interface IMqttService
    {
        Task ConnectAsync(string broker, int port, string topic);
        Task DisconnectAsync();
        Task<bool> Publish(string topic, string message, string broker, int port);
        event Action<byte[]> OnImageReceived;
    }
    public class MqttService: IMqttService
    {
        private readonly IMqttClient _mqttClient;
        public event Action<byte[]> OnImageReceived;
        const string password = "801490";

        public MqttService()
        {
            var mqttFactory = new MqttClientFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
        }

        public async Task ConnectAsync(string broker, int port, string topic)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithCredentials("your_username", password)
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += mqttClient_ApplicationMessageReceivedAsync;

            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(options);
            }
            
            await _mqttClient.SubscribeAsync(topic);
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

        public async Task<bool> Publish(string topic, string message, string broker, int port)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(broker, port)
                    .WithCredentials("your_username", password)
                    .Build();
            if(!_mqttClient.IsConnected)
            {
                return false;
            }
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            return true;
            //Console.WriteLine("MQTT application message is published.");
            //await DisconnectAsync();
        }
    }
}
