using Android.App;
using Android.Content;
using Microsoft.Identity.Client;


namespace MauiCamMqttClient.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal96e4803a-1953-402d-8083-4cfc9a85b29d")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
