using MqttClientService;
using ViewModelLayer;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraSettingsViewModel:BaseViewModel
    {
        private readonly IMqttService _mqttService;
        public bool IsFlash
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                if (value)
                {
                    await publishMQTTMessage("flash", "ON");
                }
                else
                {
                    await publishMQTTMessage("flash", "OFF");
                }

            });
        }
        public bool Flip
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                if (value)
                {
                    await publishMQTTMessage("flip", "ON");
                }
                else
                {
                    await publishMQTTMessage("flip", "OFF");
                }
            });
        }
        public bool Mirror
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                if (value)
                {
                    await publishMQTTMessage("mirror", "ON");
                }
                else
                {
                    await publishMQTTMessage("mirror", "OFF");
                }
            });
        }
        public int Brightness
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                await publishMQTTMessage("brightness", value.ToString());
            });
        }
        public int Contrast
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                await publishMQTTMessage("contrast", value.ToString());
            });
        }
        public int Saturation
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                await publishMQTTMessage("saturation", value.ToString());
            });
        }
        public int Quality
        {
            get => field;
            set => UpdateObservable(ref field, value, async () =>
            {
                await publishMQTTMessage("quality", value.ToString());
            });
        }
        public CameraSettingsViewModel(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }
        private string getTopicName()
        {
            string[] topicName = ServiceLocator.CameraComboBoxItemViewModel.Items.SelectedItem.BaseTopicName.Split('/');

            return topicName[topicName.Length - 1];
        }   
        private async Task<bool> publishMQTTMessage(string topicHead, string message)
        {
            bool isPublished;
            if (!ServiceLocator.CameraComboBoxItemViewModel.Items.IsSelected)
            {
                await Shell.Current.DisplayAlert("Error", "Please select a camera", "OK");
                return false;
            }
            MqttData mqttData = new MqttData(ServiceLocator.CameraComboBoxItemViewModel.Items.SelectedItem);
            mqttData.P_TopicName = $"camera/{getTopicName()}/{topicHead}";
            mqttData.Message = message;
            isPublished = await _mqttService.Publish(mqttData);
            if (!isPublished)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to publish the message. Please connect it first", "OK");
                return false;
            }
            return true;
        }
    }
}
