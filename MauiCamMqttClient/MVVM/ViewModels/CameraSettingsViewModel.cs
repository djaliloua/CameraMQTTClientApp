namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraSettingsViewModel:BaseViewModel
    {
        private readonly IMqttService _mqttService;
        private const int Port = 1883;
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
            string[] topicName = ServiceLocator.CameraComboBoxItemViewModel.SelectedItem.TopicName.Split('/');

            return topicName[topicName.Length - 1];
        }   
        private async Task<bool> publishMQTTMessage(string topicHead, string message)
        {
            bool isPublished;
            if (!ServiceLocator.CameraComboBoxItemViewModel.IsSelected)
            {
                await Shell.Current.DisplayAlert("Error", "Please select a camera", "OK");
                return false;
            }
            isPublished = await _mqttService.Publish($"camera/{getTopicName()}/{topicHead}", message, ServiceLocator.CameraComboBoxItemViewModel.SelectedItem.HostName, Port);
            if (!isPublished)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to publish the message. Please connect it first", "OK");
            }
            return true;
        }
    }
}
