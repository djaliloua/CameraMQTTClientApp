using MqttClientService;
#if ANDROID
using MauiCamMqttClient.MVVM.Views.BottomSheet;
using MauiCamMqttClient.Platforms.Android;
using System.Diagnostics;
#endif


namespace MauiCamMqttClient
{
    public partial class MainPage : ContentPage
    {
        private readonly IMqttService _mqttService;
        private const string Broker = "20.208.128.223"; //"103-45-247-7.cloud-xip.com"; //"192.168.1.131";
        private const int Port = 1883;
        private const string Topic = "camera/image";
        private byte[] _currentFrame;

        //
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;
        public MainPage()
        {
            InitializeComponent();
            _mqttService = ServiceLocator.MqttService;
            _mqttService.OnImageReceived += OnImageReceived;
            VideoCanvas.Drawable = new FrameDrawable(() => _currentFrame);
            VideoCanvas.Invalidate();
            KeepScreenOn();
        }

        private void KeepScreenOn()
        {
#if ANDROID
            // Request the permission
            ScreenControl.KeepScreenOn();
            //var bottom = new CameraSettings();
            //bottom.ShowAsync();
#endif
        }
#if ANDROID
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Optionally allow the screen to sleep again when leaving the page
            ScreenControl.AllowScreenSleep();
        }
#endif
        private void OnImageReceived(byte[] imageData)
        {
            _currentFrame = imageData;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                VideoCanvas.Invalidate(); // Triggers a redraw
            });
        }

        private class FrameDrawable : IDrawable
        {
            private readonly Func<byte[]> _getFrame;

            public FrameDrawable(Func<byte[]> getFrame)
            {
                _getFrame = getFrame;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                var frame = _getFrame();
                if (frame == null) return;
                using var stream = new MemoryStream(frame);
                var bitmap = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
                canvas.DrawImage(bitmap, dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
            }
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = Math.Clamp(targetX, -Content.Width * (currentScale - 1), 0);
                Content.TranslationY = Math.Clamp(targetY, -Content.Height * (currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }
    }
    public class PinchToZoomContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PinchToZoomContainer()
        {
            PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = Math.Clamp(targetX, -Content.Width * (currentScale - 1), 0);
                Content.TranslationY = Math.Clamp(targetY, -Content.Height * (currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }
    }

}