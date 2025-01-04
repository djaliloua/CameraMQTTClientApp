using MauiCamMqttClient.DataAccess.Implementation;
using MauiCamMqttClient.Extensions;
using MauiCamMqttClient.MVVM.Views;
using Patterns.Abstractions;
using Patterns.Implementations;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraViewModel : BaseViewModel, IDataErrorInfo, IClone<CameraViewModel>
    {
        public int Id
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string Name
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string TopicName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string HostName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public bool IsActive
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public CameraViewModel()
        {

        }
        public override string ToString() => Name + " - " + TopicName;
        public CameraViewModel Clone() => MemberwiseClone() as CameraViewModel;

        #region Validation
        public string Error
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return "Name is required";
                }
                if (string.IsNullOrWhiteSpace(TopicName))
                {
                    return "TopicName is required";
                }
                if (string.IsNullOrWhiteSpace(HostName))
                {
                    return "HostName is required";
                }
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        break;
                    case nameof(TopicName):
                        break;
                }
                if (string.IsNullOrWhiteSpace(Name))
                {
                    result = "Name is required";
                }
                if (string.IsNullOrWhiteSpace(TopicName))
                {
                    result = "TopicName is required";
                }
                if (string.IsNullOrWhiteSpace(HostName))
                {
                    result = "HostName is required";
                }
                return result;
            }
        }
        #endregion
    }
    public class LoadCameraService : ILoadService<CameraViewModel>
    {
        public IList<CameraViewModel> Reorder(IList<CameraViewModel> items)
        {
            return items.OrderByDescending(a => a.Id).ToList();
        }
    }
    public class CameraComboBoxItemViewModel : Loadable<CameraViewModel>
    {
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public CameraComboBoxItemViewModel(ILoadService<CameraViewModel> loadService) : base(loadService)
        {
            Init();
            UpdateCommand = new Command(OnUpdate);
            DeleteCommand = new Command(OnDelete);
        }
        protected override int Index(CameraViewModel item)
        {
            CameraViewModel cameraViewModel = Items.FirstOrDefault(x => x.Id == item.Id);
            return base.Index(cameraViewModel);
        }
        private async void OnDelete(object parameter)
        {
            CameraViewModel cameraViewModel = (CameraViewModel)parameter;
            bool result = await Shell.Current.DisplayAlert("Info", $"Do you want to delete {cameraViewModel.Name}", "Yes", "No");
            if (result)
            {
                using CameraRepository cameraRepository = new CameraRepository();
                cameraRepository.Delete(cameraViewModel.Id);
                DeleteItem(cameraViewModel);
            }
        }
        private async void OnUpdate(object parameter)
        {
            CameraViewModel viewModel = parameter as CameraViewModel;
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"IsSave", false },
                {"CamVM", viewModel.Clone() },
            };
            await Shell.Current.GoToAsync(nameof(CameraForm), parameters);
        }

        private async void Init()
        {
            using CameraRepository _repository = new();
            await LoadItems(_repository.GetAllToViewModel());
            // Initialize with object
            if (Counter > 1)
            {
                SelectedItem = Items[0];
            }
        }
    }
}
