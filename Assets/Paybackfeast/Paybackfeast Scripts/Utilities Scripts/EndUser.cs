using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class EndUser : MonoBehaviour
{
    public string edu_id;
    public bool sign_up;
    public string sign_up_dt;
    public string mail;
    public int pin;
    public string edu_name;
    public string pwd;
    public bool log_in;
    public string log_in_dt;
    public string log_out_dt;
    public string message;

    public async Task EndUserLogin(string url, string mail, string password)
    {
        try
        {
            using (var webRequest = UnityWebRequest.Get($"{url}?mail={mail}&password={password}"))
            {
                webRequest.SendWebRequest();
                await Task.Delay(1000);
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    EndUser endUser = JsonConvert.DeserializeObject<EndUser>(webRequest.downloadHandler.text);
                    Debug.Log($"End User message {endUser.message}");
                }
                else
                {
                    Debug.LogError($"Failed to read the web request");
                }
                webRequest.Dispose();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async Task GetEndUserByMail(string url, string mail)
    {
        try
        {
            using (var webRequest = UnityWebRequest.Get($"{url}?mail={mail}"))
            {
                webRequest.SendWebRequest();
                await Task.Delay(1000);
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    EndUser endUser = JsonConvert.DeserializeObject<EndUser>(webRequest.downloadHandler.text);
                    Debug.Log($"End User message {endUser.message}");
                }
                else
                {
                    Debug.LogError($"Failed to read the web request");
                }
                webRequest.Dispose();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
