using Patterns.Abstractions;
using Patterns.Implementations;
using System.ComponentModel;
using ViewModelLayer.DataAccessLayer;

namespace ViewModelLayer
{
    public class CredentialViewModel:BaseViewModel
    {
        public int Id
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string UserName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string Password
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public int CameraId
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public CredentialViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public CredentialViewModel()
        {
            
        }
        public bool IsValidate()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            return true;
        }
    }
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
        public CredentialViewModel Credential
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string Port
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string PublishTopic
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public CameraViewModel()
        {
            Port = "1883";
            Credential = new CredentialViewModel("your_username", "801490");
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
    public class CollectionViewModel : Loadable<CameraViewModel>
    {
        
        public CollectionViewModel(ILoadService<CameraViewModel> loadService) : base(loadService)
        {
            Init();
            
        }
        protected override int Index(CameraViewModel item)
        {
            CameraViewModel cameraViewModel = Items.FirstOrDefault(x => x.Id == item.Id);
            return base.Index(cameraViewModel);
        }
        public bool Delete(CameraViewModel camera)
        {
            using CameraRepository _repository = new();
            _repository.Delete(camera.Id);
            DeleteItem(camera);
            return true;
        }
        public bool Update(CameraViewModel camera)
        {
            using CameraRepository _repository = new();
            _repository.Update(camera);
            UpdateItem(camera);
            return true;
        }
        public bool Add(CameraViewModel camera)
        {
            using CameraRepository _repository = new();
            CameraViewModel cameraViewModel = _repository.Save(camera);
            AddItem(cameraViewModel);
            return true;
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
