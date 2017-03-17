# carnotify

## Adjust ConfigurationConstants.cs
Adjust the strings in the File `.\carnotify\carnotify\carnotify\Constants\ConfigurationConstants.cs`

```cs
public static class ConfigurationConstants
{
    // Enpoint-Url to Backend for Registration of a new Plate
    public static string RegisterCarEndpoint { get => throw new NotImplementedException();  } 
    // Function API-Key for RegisterNewCar
    public static string RegisterCarKey { get => throw new NotImplementedException(); }
    // Enpoint-Url to Backend for Notification of a Plate
    public static string NotifyCarEndpoint { get => throw new NotImplementedException(); }
    // Function API-Key for NotifyCar
    public static string NotifyCarKey { get => throw new NotImplementedException(); }
    // Connectionstring for Notification-Hub with at least Listen-Permission
    public static string NotificationHubListenConnectionString { get => throw new NotImplementedException(); }
    // Namespace of the NotificationHub
    public static string NotificationHubHubName { get => throw new NotImplementedException(); }
    // API-Key for Vision-API
    public static string VisionApiApiKey { get => throw new NotImplementedException(); }
    // API-Key for MobileCenter-Android-Crash-Analytics
    public static string MobileCenterAndoirdKey { get => throw new NotImplementedException(); }
    // API-Key for MobileCenter-iOS-Crash-Analytics
    public static string MobileCenteriOSKey { get => throw new NotImplementedException(); }
    // SenderId of FCM
    public static string GCMSenderId { get => throw new NotImplementedException(); }
}
```

## Add Certificate for the UWP-Application
Double-click the `Package.appxmanifest` in the UWP Porject from within Visual Studio. 
Click on `Packaging` --> `Choose Certificate`.
From `Configure Certificate...` choose `Create test certificate...`.
Provide a `Publisher Common Name` and hit `OK` and then `OK`.
Save the `Package.appxmanifest`.

## Push-Notifications with Azure Notification Hub

This are the resources to lookup Setup of Notifications
1. [Android](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-android-push-notification-google-fcm-get-started)
2. [iOS](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-ios-apple-push-notification-apns-get-started)
3. [Chrome](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-chrome-push-notifications-get-started)
4. [Xamarin.iOS](https://docs.microsoft.com/en-us/azure/notification-hubs/xamarin-notification-hubs-ios-push-notification-apns-get-started)
5. [Xamarin.Android](https://docs.microsoft.com/en-us/azure/notification-hubs/xamarin-notification-hubs-push-notifications-android-gcm)
6. [UWP](https://docs.microsoft.com/en-us/azure/notification-hubs/notification-hubs-windows-store-dotnet-get-started-wns-push-notification)
7. [Xamarin.Forms](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-push)


## Xamarin Setups
[Latest Preview 4.3.1.39](http://dl.xamarin.com/XamarinforVisualStudio/Windows/Xamarin.VisualStudio_4.3.1.39.msi)