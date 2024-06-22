using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifierDelegate(string message);

public class Notifier 
{
    public NotifierDelegate OnNotified; 

    public Notifier()
    {
        this.OnNotified += UpdateNotification;
        

    }
    
    void UpdateNotification(string message)
    {
        Debug.Log($"{nameof(UpdateNotification)} \n message {message}");
    }
    


}
