using Camera.MAUI;

namespace MauiCamMqttClient.Pages;

public partial class QRCode : ContentPage
{
	public QRCode()
	{
		InitializeComponent();
        camera.BarCodeOptions = new BarcodeDecodeOptions()
        {
            PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE, BarcodeFormat.CODE_39 }
        };
        camera.BarcodeDetected += cameraView_BarcodeDetected;
    }
    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {

        if (camera.NumCamerasDetected > 0)
        {
            camera.Camera = camera.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (await camera.StartCameraAsync() == CameraResult.Success)
                {

                }
            });
        }
    }

    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //Debug.WriteLine("Barcode detected: " + args.Result[0].Text);
            await DisplayAlert("Barcode detected", args.Result[0].Text, "OK");
        });
    }

}