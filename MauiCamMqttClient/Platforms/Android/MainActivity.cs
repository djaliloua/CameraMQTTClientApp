using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Plugin.Fingerprint;

namespace MauiCamMqttClient
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            // Check the orientation
            switch (newConfig.Orientation)
            {
                case Orientation.Portrait:
                    // Logic for portrait mode
                    Console.WriteLine("Device rotated to Portrait mode.");
                    //WeakReferenceMessenger.Default.Send("portrait", "orientation");
                    ServiceLocator.MainViewModel.IsLandScape = true;
                    break;
                case Orientation.Landscape:
                    // Logic for landscape mode
                    Console.WriteLine("Device rotated to Landscape mode.");
                    //WeakReferenceMessenger.Default.Send("landscape", "orientation");
                    ServiceLocator.MainViewModel.IsLandScape = false;
                    break;
            }
        }
    }
}
