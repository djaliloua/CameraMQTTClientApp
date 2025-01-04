using static MauiCamMqttClient.MVVM.ViewModels.CameraViewModel;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraListViewModel : BaseViewModel
    {
        public CameraComboBoxItemViewModel ListOfCamera { get; private set; }
        public CameraListViewModel()
        {
            ListOfCamera = ServiceLocator.CameraComboBoxItemViewModel;
        }
    }
}
