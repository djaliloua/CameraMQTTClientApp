using BaseViewModels;
using BaseViewModels.BaseModel;
using MauiCamMqttClient.MVVM.Views;
using System.Windows.Input;
using ViewModelLayer;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraListViewModel : BaseViewModel
    {
        public CollectionViewModel ListOfCamera { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public CameraListViewModel()
        {
            ListOfCamera = ServiceLocator.CollectionViewModel;
            UpdateCommand = new Command(OnEdit);
            DeleteCommand = new Command(OnDelete);
        }

        private async void OnDelete(object parameter)
        {
            MQTTConfigViewModel cameraViewModel = (MQTTConfigViewModel)parameter;
            bool result = await Shell.Current.DisplayAlert("Info", $"Do you want to delete {cameraViewModel.Name}", "Yes", "No");
            if (result)
            {
                //await ListOfCamera.Delete(cameraViewModel);
#if DEBUG
                ListOfCamera.Delete(cameraViewModel, ServiceLocator.MQTTConfigContext);
#else
                                ListOfCamera.Delete(cameraViewModel);
#endif
            }
        }

        private async void OnEdit(object parameter)
        {
            MQTTConfigViewModel viewModel = parameter as MQTTConfigViewModel;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"IsSave", false },
                {"CamVM", viewModel.Clone() },
            };
            await Shell.Current.GoToAsync(nameof(CameraForm), parameters);
        }
    }
}
