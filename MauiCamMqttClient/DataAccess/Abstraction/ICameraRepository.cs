using MauiCamMqttClient.MVVM.ViewModels;
using Models;
using Repository.Interface;
using ViewModelLayer;

namespace MauiCamMqttClient.DataAccess.Abstraction
{
    public interface ICameraRepository : IGenericRepository<Camera>
    {
        IList<CameraViewModel> GetAllDtos();
    }
}
