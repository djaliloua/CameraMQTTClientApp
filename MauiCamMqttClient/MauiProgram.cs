using MauiCamMqttClient.Extensions;
using Microsoft.Extensions.Logging;
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System.Diagnostics;
using The49.Maui.BottomSheet;
using Camera.MAUI;
using UraniumUI;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Identity.Client;

namespace MauiCamMqttClient
{
    public static class MauiProgram
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .LoadBIExtension()
                .ViewModelsExtension()
                .UtilityExtension()
                .ContextExtension()
                .RepositoryExtension()
                .UseBottomSheet()
                .UseMauiCameraView()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                //.UseCameraScanner()
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(platform =>
                    {
                        platform.OnActivityResult((activity, rc, result, data) =>
                        {
                            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(rc, result, data);
                        });
                    });
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });
             

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
            ServiceProvider = builder.Services.BuildServiceProvider();
            if(!Debugger.IsAttached)
            {
                FingerPrintAuthentification _authentification = new FingerPrintAuthentification();
                _ = _authentification.Authenticate();
            }
            MauiApp app = builder.Build();
            app.RunSeedData();


            return app;
        }
    }
}
