using Xamarin.Forms;

namespace carnotify
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform ==  Device.iOS)
            {
                registerCarPage.Icon = new FileImageSource { File = "iconcar.png" };
                notifyCarPage.Icon = new FileImageSource { File = "iconspeaker.png" };
            }
        }
    }
}
