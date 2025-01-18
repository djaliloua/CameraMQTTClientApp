using BaseViewModels;
using Mapster;
using MauiCamMqttClient.MVVM.ViewModels;
using MauiCamMqttClient.MVVM.ViewModels.Pages;
using MauiIcons.Material;
using Models;
using MqttClientService;
using Patterns.Abstractions;
using ViewModelLayer;

namespace MauiCamMqttClient.Extensions
{
    public static class ViewModelExtensions
    {
        public static MQTTConfigViewModel ToViewModel(this MQTTConfig item) => item.Adapt<MQTTConfigViewModel>();
        public static MQTTConfig FromViewModel(this MQTTConfigViewModel item) => item.Adapt<MQTTConfig>();
        public static TDestination ToViewModel<TSource, TDestination>(this TSource model) => model.Adapt<TDestination>();
        public static IList<TDestination> ToVM<TSource, TDestination>(this IList<TSource> model) => model.Adapt<List<TDestination>>();
    }
    public static class Extensions
    {
        public static MauiAppBuilder PagesExtensions(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MainPage>();

            return mauiAppBuilder;
        }
        public static MauiAppBuilder RepositoryExtension(this MauiAppBuilder mauiAppBuilder)
        {
            //mauiAppBuilder.Services.AddTransient<ICameraRepository, CameraRepository>();

            return mauiAppBuilder;
        }
        public static MauiAppBuilder ContextExtension(this MauiAppBuilder mauiAppBuilder)
        {
            // Maui App Builder that Comes with Default Maui App
            mauiAppBuilder.UseMauiApp<App>()
                // Initialises the .Net Maui Icons - Material
                .UseMaterialMauiIcons();
            return mauiAppBuilder;
        }
        public static MauiAppBuilder ViewModelsExtension(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<MQTTConfigViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraComboBoxItemViewModel>();
            mauiAppBuilder.Services.AddSingleton<MainViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraFormViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraListViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraSettingsViewModel>();
            mauiAppBuilder.Services.AddSingleton<CollectionViewModel>();
            mauiAppBuilder.Services.AddSingleton<MQTTConfigFormViewModel>();

            return mauiAppBuilder;
        }
        public static MauiAppBuilder UtilityExtension(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddMapster();
            mauiAppBuilder.Services.AddSingleton<IMqttService, MqttService>();
            return mauiAppBuilder;
        }
        public static MauiAppBuilder LoadBIExtension(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddScoped<ILoadService<MQTTConfigViewModel>, LoadCameraService>();
            return mauiAppBuilder;
        }
    }

}
