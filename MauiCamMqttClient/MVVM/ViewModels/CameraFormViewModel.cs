using MauiCamMqttClient.DataAccess.Implementation;
using MauiCamMqttClient.Extensions;
using Models;
using System.Windows.Input;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraFormViewModel:BaseViewModel, IQueryAttributable
    {
        public CameraViewModel CameraFrm
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
       
        public bool IsSave
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        #region Commands
        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        #endregion

        public CameraFormViewModel()
        {
            AddCommand = new Command(OnAdd);
            UpdateCommand = new Command(OnUpdate);
        }
        private async void OnUpdate(object parameter)
        {
            if (CameraFrm.Error == null)
            {
                ServiceLocator.CameraComboBoxItemViewModel.UpdateItem(CameraFrm);
                using CameraRepository cameraRepository = new CameraRepository();
                Camera cam = cameraRepository.Update(CameraFrm.FromDto());
                ServiceLocator.CameraComboBoxItemViewModel.UpdateItem(cam.ToDto());
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Info", CameraFrm.Error, "Cancel");
            }
        }
        private async void OnAdd(object parameter)
        {
            if (CameraFrm.Error == null)
            {
                if (!CheckDuplicateName(CameraFrm.Name))
                {
                    using CameraRepository cameraRepository = new CameraRepository();
                    Camera cam = cameraRepository.Save(CameraFrm.FromDto());
                    ServiceLocator.CameraComboBoxItemViewModel.AddItem(cam.ToDto());
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Info", "Please, change that name", "Cancel");
                }

            }
            else
            {
                await Shell.Current.DisplayAlert("Info", CameraFrm.Error, "Cancel");
            }

        }
        private static bool CheckDuplicateName(string name)
        {
            CameraViewModel viewModel = ServiceLocator.CameraComboBoxItemViewModel.Items.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
            return viewModel != null;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.ContainsKey("IsSave"))
            {
                IsSave = (bool)query["IsSave"];
                CameraFrm = (CameraViewModel)query["CamVM"];
            }
        }
    }
}
