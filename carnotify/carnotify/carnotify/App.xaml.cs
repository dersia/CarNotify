using carnotify.Interfaces;
using carnotify.Services;
using carnotify.ViewModels;
using Prism.Unity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile;
using carnotify.Constants;

namespace carnotify
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();
        }

        protected async override void OnInitialized()
        {
            await NavigationService.NavigateAsync($"carnotify://NavigationPage/{nameof(MainPage)}/{nameof(RegisterCarPage)}");
        }

        protected override void RegisterTypes()
        {
            // Register Services
            Container.RegisterType<ICarNotifyService, CarNotifyService>();

            // Register Pages
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
            Container.RegisterTypeForNavigation<RegisterCarPage, RegisterCarPageViewModel>();
            Container.RegisterTypeForNavigation<NotifyCarPage, NotifyCarPageViewModel>();
        }

        protected override void OnStart()
        {
            MobileCenter.Start($"ios={ConfigurationConstants.MobileCenteriOSKey};" +
                   $"android={ConfigurationConstants.MobileCenterAndoirdKey};",
                   typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
