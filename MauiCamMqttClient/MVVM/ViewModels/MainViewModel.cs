using MauiCamMqttClient.MVVM.Views;
using MauiCamMqttClient.MVVM.Views.BottomSheet;
using MqttClientService;
using System.Windows.Input;
using ViewModelLayer;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private readonly IMqttService _mqttService;
        private const int Port = 1883;
        public CameraComboBoxItemViewModel CameraComboBoxItemViewModel
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        #region Commands
        public ICommand NewCommand { get; private set; }
        public ICommand ShowAllCommand { get; private set; }
        public ICommand StartStreamCommand { get; private set; }
        public ICommand StopStreamCommand { get; private set; }
        public ICommand ShowCamSettingCommand { get; private set; }
        #endregion

        #region Constructor
        public MainViewModel(IMqttService mqttService)
        {
            CameraComboBoxItemViewModel = ServiceLocator.CameraComboBoxItemViewModel;
            _mqttService = mqttService;
            NewCommand = new Command(OnNew);
            ShowAllCommand = new Command(OnShowAll);
            StartStreamCommand = new Command(OnStartStream);
            StopStreamCommand = new Command(OnStopStream);
            ShowCamSettingCommand = new Command(OnShowCamSetting);
        }
        #endregion

        private async void OnShowCamSetting(object parameter)
        {
            CameraSettings cameraSettings = new CameraSettings();
            cameraSettings.HasHandle = true;
            cameraSettings.HandleColor = Color.FromArgb("#FF4081");
            await cameraSettings.ShowAsync();
        }

        
        private async void OnStopStream(object obj)
        {
            try
            {
                if (!CameraComboBoxItemViewModel.Items.IsSelected)
                {
                    await Shell.Current.DisplayAlert("Error", "Please select a camera", "OK");
                    return;
                }
                ServiceLocator.CameraSettingsViewModel.IsFlash = false;
                await _mqttService.DisconnectAsync();
                await Shell.Current.DisplayAlert("Disconnected", "Stopped the stream!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnStartStream(object obj)
        {
            try
            {
                if (!CameraComboBoxItemViewModel.Items.IsSelected)
                {
                    await Shell.Current.DisplayAlert("Error", "Please select a camera", "OK");
                    return;
                }
                MqttData mqttData = new MqttData(CameraComboBoxItemViewModel.Items.SelectedItem);
                await _mqttService.ConnectAsync(mqttData);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnShowAll(object parameter)
        {
            await Shell.Current.GoToAsync(nameof(CameraList));
        }

        private async void OnNew(object parameter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"IsSave", true },
                {"CamVM", new CameraViewModel() },
            };
            await Shell.Current.GoToAsync(nameof(CameraForm), parameters);
        }
    }
}
