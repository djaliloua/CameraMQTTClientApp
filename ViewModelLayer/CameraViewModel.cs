using BaseViewModels;
using Patterns.Abstractions;
using Patterns.Implementations;
using RepositoryEntityVMWrapper.Abstractions;
using RepositoryEntityVMWrapper.Implementations;

namespace ViewModelLayer
{
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
}
