using System.Windows.Input;
using ViewModelLayer;
using KiotaOpenAIClient;
using MauiCamMqttClient.Extensions;
using KiotaOpenAIClient.Client.Models;

namespace MauiCamMqttClient.MVVM.ViewModels.Pages
{
    public class MQTTConfigFormViewModel:BaseViewModel
    {
        public MQTTConfigViewModel MQTTConfig
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public ICommand SaveCommand { get; private set; }
        public MQTTConfigFormViewModel()
        {
            Init();
            SaveCommand = new Command(OnSave);
        }
        private async void Init()
        {
            IApiService apiService = new ApiService();
            var config = await apiService.GetMQTTConfigByGuidAsync(Guid.Parse("3FA85F64-5717-4562-B3FC-2C963F66AFA6"));
            MQTTConfig = config.ToViewModel<MQTTConfig, MQTTConfigViewModel>() ?? new MQTTConfigViewModel();
        }

        private void OnSave(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
