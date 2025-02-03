using BaseViewModels.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseViewModels
{
    public class MQTTConfigViewModel : BaseViewModel, IDataErrorInfo, IClone<MQTTConfigViewModel>
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
    public class MQTTCredentialViewModel : BaseViewModel
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
        public MQTTCredentialViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public MQTTCredentialViewModel()
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
}
