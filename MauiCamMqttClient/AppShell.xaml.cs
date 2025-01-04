using MauiCamMqttClient.MVVM.Views;

namespace MauiCamMqttClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CameraForm), typeof(CameraForm));
            Routing.RegisterRoute(nameof(CameraList), typeof(CameraList));
        }
    }
}
