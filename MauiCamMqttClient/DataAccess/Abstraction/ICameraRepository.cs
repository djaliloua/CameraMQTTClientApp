using MauiCamMqttClient.MVVM.ViewModels;
using Models;
using Repository.Interface;

namespace MauiCamMqttClient.DataAccess.Abstraction
{
    public interface ICameraRepository : IGenericRepository<Camera>
    {
        IList<CameraViewModel> GetAllDtos();
    }
}
