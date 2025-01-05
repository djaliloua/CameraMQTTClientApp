using MauiCamMqttClient.MVVM.ViewModels;
using MqttClientService;
using ViewModelLayer;

namespace MauiCamMqttClient
{
    public static class ServiceLocator
    {
        public static CollectionViewModel CollectionViewModel => GetService<CollectionViewModel>();
        public static IMqttService MqttService => GetService<IMqttService>();
        public static CameraSettingsViewModel CameraSettingsViewModel => GetService<CameraSettingsViewModel>();
        public static CameraListViewModel CameraListViewModel => GetService<CameraListViewModel>();
        public static CameraFormViewModel CameraFormViewModel => GetService<CameraFormViewModel>();
        public static CameraComboBoxItemViewModel CameraComboBoxItemViewModel => GetService<CameraComboBoxItemViewModel>();
        public static CameraViewModel CameraViewModel => GetService<CameraViewModel>();
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static T GetService<T>() => MauiProgram.ServiceProvider.GetService<T>();
    }
}
