using Android.Views;

namespace MauiCamMqttClient.Platforms.Android
{
    public static class ScreenControl
    {
        public static void KeepScreenOn()
        {
            var activity = Platform.CurrentActivity;
            activity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
        }

        public static void AllowScreenSleep()
        {
            var activity = Platform.CurrentActivity;
            activity.Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
        }
    }
}
