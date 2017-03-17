using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;

namespace carnotify.Droid
{
    [Activity(Label = "carnotify", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            InitNotifications();
        }

        private void InitNotifications()
        {
            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message, "Error");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message, "Error");
            }
        }
    }
}

