using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class MobileNotificationManager : MonoBehaviour
{
    public static MobileNotificationManager Instance;
    AndroidNotificationChannel notifChanel;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();
       
        notifChanel = new AndroidNotificationChannel
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notifications",         
            Description = "Reminders Login",
            Importance = Importance.High,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChanel);

        DisplayNotification("Come Back!", "You have been gone for a while, come back and play!", IconSelecter.icon_0,
         IconSelecter.icon_0,
          DateTime.Now.AddDays(1));
    }
    public int DisplayNotification(string title, string text, IconSelecter iconSmall,IconSelecter iconLarge, 
        DateTime firetime)
    {
            var notification = new AndroidNotification();
        
            notification.Title = title;
            notification.Text = text;
            notification.SmallIcon = iconSmall.ToString();
            notification.LargeIcon = iconLarge.ToString();
            notification.FireTime = firetime;

        return AndroidNotificationCenter.SendNotification(notification, notifChanel.Id);
    }
    public void CancelNotification (int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);      
    }

}
public enum IconSelecter
{
    icon_0,
    icon_1   
}