using Patterns.Abstractions;
using Patterns.Implementations;
using System.ComponentModel;
using ViewModelLayer.DataAccessLayer;

namespace ViewModelLayer
{
    public class CredentialViewModel : BaseViewModel
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
    //public class CameraViewModel : BaseViewModel, IDataErrorInfo, IClone<CameraViewModel>
    //{
    //    public int Id
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public string Name
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public string TopicName
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public string HostName
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public bool IsActive
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public CredentialViewModel Credential
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public string Port
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public string PublishTopic
    //    {
    //        get => field;
    //        set => UpdateObservable(ref field, value);
    //    }
    //    public CameraViewModel()
    //    {
    //        Port = "1883";
    //        Credential = new CredentialViewModel("your_username", "801490");
    //    }

    //    public override string ToString() => Name + " - " + TopicName;
    //    public CameraViewModel Clone() => MemberwiseClone() as CameraViewModel;

    //    #region Validation
    //    public string Error
    //    {
    //        get
    //        {
    //            if (string.IsNullOrWhiteSpace(Name))
    //            {
    //                return "Name is required";
    //            }
    //            if (string.IsNullOrWhiteSpace(TopicName))
    //            {
    //                return "TopicName is required";
    //            }
    //            if (string.IsNullOrWhiteSpace(HostName))
    //            {
    //                return "HostName is required";
    //            }
    //            return null;
    //        }
    //    }

    //    public string this[string columnName]
    //    {
    //        get
    //        {
    //            string result = string.Empty;
    //            switch (columnName)
    //            {
    //                case nameof(Name):
    //                    break;
    //                case nameof(TopicName):
    //                    break;
    //            }
    //            if (string.IsNullOrWhiteSpace(Name))
    //            {
    //                result = "Name is required";
    //            }
    //            if (string.IsNullOrWhiteSpace(TopicName))
    //            {
    //                result = "TopicName is required";
    //            }
    //            if (string.IsNullOrWhiteSpace(HostName))
    //            {
    //                result = "HostName is required";
    //            }
    //            return result;
    //        }
    //    }
    //    #endregion
    //}
    public class LoadCameraService : ILoadService<MQTTConfigViewModel>
    {
        public IList<MQTTConfigViewModel> Reorder(IList<MQTTConfigViewModel> items)
        {
            return items.OrderByDescending(a => a.Id).ToList();
        }
    }
    public class CollectionViewModel : Loadable<MQTTConfigViewModel>
    {
        public CollectionViewModel(ILoadService<MQTTConfigViewModel> loadService) : base(loadService)
        {
            Init();
        }
        protected override int Index(MQTTConfigViewModel item)
        {
            MQTTConfigViewModel cameraViewModel = Items.FirstOrDefault(x => x.Id == item.Id);
            return base.Index(cameraViewModel);
        }
        public bool Delete(MQTTConfigViewModel camera)
        {
            using ICameraRepo _repository = new CameraRepositoryApi();
            _repository.Delete(camera.Id);
            DeleteItem(camera);
            return true;
        }
        public async Task<bool> Update(MQTTConfigViewModel camera)
        {
            using ICameraRepo _repository = new CameraRepositoryApi();
            await _repository.UpdateAsync(camera);
            UpdateItem(camera);
            return true;
        }
        public async Task<bool> Add(MQTTConfigViewModel camera)
        {
            using ICameraRepo _repository = new CameraRepositoryApi();
            MQTTConfigViewModel cameraViewModel = await _repository.SaveAsync(camera);
            AddItem(cameraViewModel);
            return true;
        }
        private async void Init()
        {
            using ICameraRepo _repository = new CameraRepositoryApi();
            await LoadItems(await _repository.GetAllToViewModel());
            // Initialize with object
            if (Counter > 0)
            {
                SelectedItem = Items[0];
            }
        }
    }
    public class MQTTConfigViewModel:BaseViewModel, IDataErrorInfo, IClone<MQTTConfigViewModel>
    {
        public int Id
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public Guid CameraId
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string HostName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string Port
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
        public string BaseTopicName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string Name
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public bool IsActive
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        #region Validation
        public string Error
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return "Name is required";
                }
                if (string.IsNullOrWhiteSpace(BaseTopicName))
                {
                    return "BaseTopicName is required";
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
                    case nameof(BaseTopicName):
                        break;
                }
                if (string.IsNullOrWhiteSpace(Name))
                {
                    result = "Name is required";
                }
                if (string.IsNullOrWhiteSpace(BaseTopicName))
                {
                    result = "BaseTopicName is required";
                }
                if (string.IsNullOrWhiteSpace(HostName))
                {
                    result = "HostName is required";
                }
                return result;
            }
        }
        #endregion

        public MQTTConfigViewModel()
        {
            Port = "1883";
            CameraId = Guid.NewGuid();
            Password = "801490";
            UserName = "your_username";
        }

        public MQTTConfigViewModel Clone() => MemberwiseClone() as MQTTConfigViewModel;
        public override string ToString() => Name;

    }
}
