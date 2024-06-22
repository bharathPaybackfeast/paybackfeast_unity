using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;


[Serializable]
public class Vendor
{
    public string vdr_id;
    public bool sign_up;
    public string sign_up_dt;
    public string mail;
    public int pin;
    public string vdr_name;
    public string pwd;
    public bool log_in;
    public string log_in_dt;
    public string log_out_dt;
    public string role;
    public string mail_otp;

    public string responseCode;
    public string message;
    public string secretKey;





    public async Task VendorLogin(string url, string mail, string password)
    {
        try
        {
            using(UnityWebRequest loginWebRequest = UnityWebRequest.Get($"{url}?mail={mail}&password={password}"))
            {
                loginWebRequest.SendWebRequest();
                await Task.Delay(1000);
                if (loginWebRequest.result == UnityWebRequest.Result.Success)
                {
                    Vendor vendorLogin = JsonConvert.DeserializeObject<Vendor>(loginWebRequest.downloadHandler.text);
                    Debug.Log($"Vendor \n Response code {vendorLogin.responseCode} \n message {vendorLogin.message}");
                    
                    // If login is successfull.
                    if (vendorLogin.responseCode == "200")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{vendorLogin.message}");

                        Authentication.OnPanel?.Invoke(4);
                    }
                    // If already logged In
                    if (vendorLogin.responseCode == "202")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{vendorLogin.message}");
                    }
                    // Incorrect input details.
                    if (vendorLogin.responseCode == "400")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{vendorLogin.message}");
                    }
                }
                else
                {
                    Authentication.OnAuthenticationMessage?.Invoke($"No details of the vendor foud. Sign up");

                }
                loginWebRequest.Dispose();

            }
        }catch (Exception e)
        {
            Debug.LogException(e);
 
        }
    }


    public async Task VendorRegister(string url, string mail, string password)
    {
        Vendor vendorRegister = new Vendor();

        vendorRegister.mail = mail;
        vendorRegister.pwd = password;

        // Serialize the Vendor object.
        string jsonOut = JsonConvert.SerializeObject(vendorRegister);

        // Create a web request.
        UnityWebRequest registerWebRequest = new UnityWebRequest($"{url}", "POST");

        // Get bytes of JSON 
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonOut);

        // Create an upload handler
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(bodyRaw);
        registerWebRequest.uploadHandler = uploadHandlerRaw;
        registerWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        // Set the content type
        registerWebRequest.SetRequestHeader("Content-Type", "application/json");

        // send the request
        registerWebRequest.SendWebRequest();

        // wait for 2 seconds
        await Task.Delay(5000);


        vendorRegister = JsonConvert.DeserializeObject<Vendor>(registerWebRequest.downloadHandler.text);
        if (vendorRegister == null)
        {
            Debug.Log("No vendor data found");
        }
        else
        {
            if (vendorRegister.responseCode == "200")
            {
                // Display message
                Authentication.OnAuthenticationMessage?.Invoke($"{vendorRegister.message}");

                // Invoke and enable login panel
                Authentication.OnPanelButton?.Invoke(0);
            }
            else if (vendorRegister.responseCode == "404")
            {
                // Display message
                Authentication.OnAuthenticationMessage?.Invoke($"{vendorRegister.message}");
            }
        }
        Debug.Log($"Response code {vendorRegister.responseCode} \n message {vendorRegister.message}");

        // Dispose the web request.
        registerWebRequest.Dispose();
    }


    public async Task VendorForgotPasswordOtpGenerator(string url, string mail)
    {
        try
        {
            bool otpReceived = false;

            using (var webRequest = UnityWebRequest.Get($"{url}?mail={mail}"))
            {
                
                webRequest.SendWebRequest();

                await Task.Yield(); 
                Debug.Log($"Task completed........ {webRequest.result}");
                
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    // Vendor vendor = JsonConvert.DeserializeObject<Vendor>(webRequest.downloadHandler.text);
                    Debug.Log($"Successfully sent the OTP to {mail} \n {webRequest.downloadHandler.text}");
                    otpReceived = true; // Set flag to true, indicating OTP is received
                    //webRequest.Dispose();
                }
                else if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError($"Failed to send the OTP to mail {mail}");
                }


            }

            
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async Task GetVendorByMail(string url, string mail)
    {
        try
        {
            using (var webRequest = UnityWebRequest.Get($"{url}?mail={mail}"))
            {
                webRequest.SendWebRequest();
                await Task.Delay(1000);
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Vendor vendor = JsonConvert.DeserializeObject<Vendor>(webRequest.downloadHandler.text);
                    Debug.Log($"Vendor message {vendor.message}");
                }
                else
                {
                    Debug.LogError($"Failed to read the web request");
                }
                webRequest.Dispose();
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
