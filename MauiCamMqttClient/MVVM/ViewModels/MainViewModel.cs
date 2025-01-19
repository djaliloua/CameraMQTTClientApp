using BaseViewModels;
using BaseViewModels.BaseModel;
using CommunityToolkit.Mvvm.Messaging;
using MauiCamMqttClient.MVVM.Views;
using MauiCamMqttClient.MVVM.Views.BottomSheet;
using MqttClientService;
using System.Diagnostics;
using System.Windows.Input;

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
        public bool IsStreaming
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public bool IsLandScape
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
#if ANDROID
            WeakReferenceMessenger.Default.Register<string>("orientation", (sender, msg) =>
            {
                if (sender.ToString() == "landscape")
                {
                    IsLandScape = false;
                }
                else
                {
                    IsLandScape = true;
                }   
            });
            IsLandScape = true;
#endif
            CameraComboBoxItemViewModel = ServiceLocator.CameraComboBoxItemViewModel;
            _mqttService = mqttService;
            NewCommand = new Command(OnNew);
            ShowAllCommand = new Command(OnShowAll);
            StartStreamCommand = new Command(OnStartStream);
            //StopStreamCommand = new Command(OnStopStream);
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
        
        private async Task OnStopStream(object obj)
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
                IsStreaming = false;
                //await Shell.Current.DisplayAlert("Disconnected", "Stopped the stream!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnStartStream(object obj)
        {
            if(IsStreaming)
            {
                await OnStopStream(obj);
                return;
            }
            try
            {
                if (!CameraComboBoxItemViewModel.Items.IsSelected)
                {
                    await Shell.Current.DisplayAlert("Error", "Please select a camera", "OK");
                    return;
                }
                MqttData mqttData = new MqttData(CameraComboBoxItemViewModel.Items.SelectedItem);
                await _mqttService.ConnectAsync(mqttData);
                IsStreaming = true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnShowAll(object parameter)
        {
            if (!Debugger.IsAttached)
            {
                FingerPrintAuthentification _authentification = new FingerPrintAuthentification();
                if(await _authentification.Authenticated())
                {
                    await Shell.Current.GoToAsync(nameof(CameraList));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "You are not authenticated", "OK");
                }
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(CameraList));
            }
            
        }

        private async void OnNew(object parameter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"IsSave", true },
                {"CamVM", new MQTTConfigViewModel() },
            };
            await Shell.Current.GoToAsync(nameof(CameraForm), parameters);
        }
    }
}
