using BaseViewModels;
using DatabaseContexts;
using Mapster;
using MauiCamMqttClient.MVVM.ViewModels;
using MauiCamMqttClient.MVVM.ViewModels.Pages;
using MauiIcons.Material;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            mauiAppBuilder.Services.AddTransient<MQTTConfigContext>();

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
            mauiAppBuilder.Services.AddSingleton<IPublicClientApplication, PublicClientApplication>();
            return mauiAppBuilder;
        }
        public static MauiAppBuilder LoadBIExtension(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddScoped<ILoadService<MQTTConfigViewModel>, LoadCameraService>();
            return mauiAppBuilder;
        }
        public static MauiApp RunSeedData(this MauiApp mauiApp)
        {
            using var scope = mauiApp.Services.CreateScope();
            var datacontext = scope.ServiceProvider.GetRequiredService<MQTTConfigContext>();
            datacontext.Database.Migrate();
            if (datacontext != null)
            {
                if (datacontext.MQTTConfigs.Count() == 0)
                {
                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "192.168.1.131",
                        CameraId = Guid.Parse("B7312B62-ACE1-4F7D-BB05-F1502FAD67C4"),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "local"
                    });

                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "20.208.128.223",
                        CameraId = Guid.Parse("B424CAA3-5F83-4F10-8A9A-13A7305FD9D4"),
                        Port = "1883",
                        BaseTopicName = "MauiCamMqttClient",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "Azure"
                    });
                    datacontext.MQTTConfigs.Add(new MQTTConfig
                    {
                        HostName = "broker.hivemq.com",
                        CameraId = Guid.Parse("8F102BDD-B337-4FC7-B24D-A493E32CFAAE"),
                        Port = "1883",
                        BaseTopicName = "video/stream/home",
                        UserName = "your_username",
                        Password = "801490",
                        Name = "Hive"
                    });

                    datacontext.SaveChanges();
                }

            }
            return mauiApp;
        }

    }
}
