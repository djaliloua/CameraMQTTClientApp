using BaseViewModels.BaseModel;
using ViewModelLayer;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraComboBoxItemViewModel :BaseViewModel
    {
        public CollectionViewModel Items
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public CameraComboBoxItemViewModel()
        {
            Items = ServiceLocator.CollectionViewModel;
        }
    }
}
