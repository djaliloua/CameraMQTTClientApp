using Mapster;
using MauiCamMqttClient.MVVM.ViewModels;
using Models;
using Patterns.Abstractions;
using static MauiCamMqttClient.MVVM.ViewModels.CameraViewModel;

namespace MauiCamMqttClient.Extensions
{
    public static class ViewModelExtensions
    {
        public static CameraViewModel ToDto(this Camera item) => item.Adapt<CameraViewModel>();
        public static Camera FromDto(this CameraViewModel item) => item.Adapt<Camera>();
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
            //mauiAppBuilder.Services.AddDbContext<RepositoryContext>(optionsAction =>
            //{
            //    optionsAction.UseSqlite("Data Source=configuration.db3");
            //    optionsAction.UseLazyLoadingProxies();
            //    optionsAction.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
            //});
            return mauiAppBuilder;
        }
        public static MauiAppBuilder ViewModelsExtension(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<CameraViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraComboBoxItemViewModel>();
            mauiAppBuilder.Services.AddSingleton<MainViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraFormViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraListViewModel>();
            mauiAppBuilder.Services.AddSingleton<CameraSettingsViewModel>();

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
            mauiAppBuilder.Services.AddScoped<ILoadService<CameraViewModel>, LoadCameraService>();


            return mauiAppBuilder;
        }
    }

}
