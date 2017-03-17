
using Android.App;
using Android.Support.V7.App;

namespace carnotify.Droid
{
    [Activity(Label = "CarNotify", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}