using BaseViewModels;
using BaseViewModels.BaseModel;
using System.Windows.Input;

namespace MauiCamMqttClient.MVVM.ViewModels
{
    public class CameraFormViewModel:BaseViewModel, IQueryAttributable
    {
        public MQTTConfigViewModel CameraFrm
        {
            get => field;
            set => UpdateObservable(ref field,  value);
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
                //
                await ServiceLocator.CameraComboBoxItemViewModel.Items.Update(CameraFrm);

                //#if DEBUG
                //                await ServiceLocator.CameraComboBoxItemViewModel.Items.Update(CameraFrm, ServiceLocator.MQTTConfigContext);

                //#else
                //                await ServiceLocator.CameraComboBoxItemViewModel.Items.Update(CameraFrm);
                //#endif
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
                    await ServiceLocator.CameraComboBoxItemViewModel.Items.Add(CameraFrm);
//#if DEBUG
//                    await ServiceLocator.CameraComboBoxItemViewModel.Items.Add(CameraFrm, ServiceLocator.MQTTConfigContext);

//#else
//                    await ServiceLocator.CameraComboBoxItemViewModel.Items.Add(CameraFrm);
//#endif
                    

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
            MQTTConfigViewModel viewModel = ServiceLocator.CameraComboBoxItemViewModel.Items.Items.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
            return viewModel != null;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.ContainsKey("IsSave"))
            {
                IsSave = (bool)query["IsSave"];
                CameraFrm = (MQTTConfigViewModel)query["CamVM"];
            }
        }
    }
}
