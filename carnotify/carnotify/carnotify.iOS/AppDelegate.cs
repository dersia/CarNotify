using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using carnotify.Services;
using System.Threading;
using carnotify.Models;
using ObjCRuntime;

namespace carnotify.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            return base.FinishedLaunching(app, options);
        }

        public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            await PushRegistrationService.RegisterDeviceAsync(cts.Token, PushPlatforms.IOS, deviceToken.ToString(), new List<string> { nameof(PushPlatforms.IOS) });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            NSDictionary alert = (userInfo.ObjectForKey(new NSString("aps")) as NSDictionary)?.ObjectForKey(new NSString("alert")) as NSDictionary;

            var msgBody = string.Empty;
            var msgTitle = string.Empty;
            if(alert != null && alert.ContainsKey(new NSString("body")))
            {
                msgBody = (alert[new NSString("body")] as NSString).ToString();
            }
            if (alert != null && alert.ContainsKey(new NSString("title")))
            {
                msgTitle = (alert[new NSString("title")] as NSString).ToString();
            }

            //show alert
            if (!string.IsNullOrEmpty(msgBody))
            {
                UIAlertView avAlert = new UIAlertView(msgTitle, msgBody, null, "OK");
                avAlert.Show();
            }
        }
    }
}
