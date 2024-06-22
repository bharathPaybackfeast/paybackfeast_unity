using System;
using Unity.Notifications.Android;
using UnityEngine.Android;


/// <summary>
/// Android Notifications
/// </summary>
public class AndroidNotifications
{

    /// <summary>
    /// Channel Id
    /// </summary>
    private static string _channelId = "channel_id_mail_otp"; 

    /// <summary>
    /// Channel name
    /// </summary>
    private static string _channelName = "Mail OTP Channel"; 

    /// <summary>
    /// Channel description
    /// </summary>
    private static string _channelDescriptiion = "Mail Notifications"; 


    private bool _notificationSet = false;


    public void SetNotificationProperties()
    {
        if(!_notificationSet) 
            return;

        SetNotificationPermission();
        SetNotificationChannel();
        _notificationSet = true;
        Console.WriteLine($"Notification properties set to {_notificationSet}");
    }
    
    /// <summary>
    /// Set notification permisssion.
    /// </summary>
    public void SetNotificationPermission()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            Console.WriteLine($"{nameof(SetNotificationPermission)}");
        }
    }

    /// <summary>
    /// Set Notification channel
    /// </summary>
    public void SetNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = _channelId,
            Name = _channelName,
            Importance = Importance.Default,
            Description = _channelDescriptiion,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        Console.WriteLine($"{nameof(SetNotificationChannel)}");
    }

    /// <summary>
    /// Sending notifications.
    /// </summary>
    public void SendNotification()
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = "Pay Later Dev V0.1.0";
        notification.Text = "Welcome to Pay Later";
        notification.FireTime = System.DateTime.Now.AddSeconds(2);

        AndroidNotificationCenter.SendNotification(notification, _channelId);
    }

}
