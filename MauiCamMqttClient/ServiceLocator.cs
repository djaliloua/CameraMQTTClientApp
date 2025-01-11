using MauiCamMqttClient.MVVM.ViewModels;
using MauiCamMqttClient.MVVM.ViewModels.Pages;
using MqttClientService;
using ViewModelLayer;

namespace MauiCamMqttClient
{
    public static class ServiceLocator
    {
        public static MQTTConfigFormViewModel MQTTConfigFormViewModel => GetService<MQTTConfigFormViewModel>();
        public static CollectionViewModel CollectionViewModel => GetService<CollectionViewModel>();
        public static IMqttService MqttService => GetService<IMqttService>();
        public static CameraSettingsViewModel CameraSettingsViewModel => GetService<CameraSettingsViewModel>();
        public static CameraListViewModel CameraListViewModel => GetService<CameraListViewModel>();
        public static CameraFormViewModel CameraFormViewModel => GetService<CameraFormViewModel>();
        public static CameraComboBoxItemViewModel CameraComboBoxItemViewModel => GetService<CameraComboBoxItemViewModel>();
        public static MQTTConfigViewModel CameraViewModel => GetService<MQTTConfigViewModel>();
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static T GetService<T>() => MauiProgram.ServiceProvider.GetService<T>();
    }
}
