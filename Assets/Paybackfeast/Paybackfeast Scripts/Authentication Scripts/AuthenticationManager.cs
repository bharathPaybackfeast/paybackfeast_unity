using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class VendorAuth : AuthenticationManager
{
    Vendor vendor;

    public VendorAuth()
    {
        vendor = new Vendor();
    }

    public override void LoginOnClick(string mail, string password)
    {
        // Utility.VendorLoginMailUrl
        Task vendorLogin = vendor.VendorLogin(Utility.PayLaterLoginMailUrl, mail, password);
        Debug.Log($"Vendor login {vendorLogin}");
    }

    public override void RegisterOnSubmit<Vendor>(string mail, string password) 
    {
        
        Task vendorRegister = vendor.VendorRegister(Utility.VendorRegisterMailUrl, mail, password);
        
        Debug.Log($"Vendor login {nameof(RegisterOnSubmit)} \n Vendor Register {vendorRegister}");
        
    }

    public override void ForgotPasswordOtpGenerator(string mail)
    {
       Task _ = vendor.VendorForgotPasswordOtpGenerator(Utility.VendorForgotPasswordOtpUrl, mail.Trim());
    }

    public override void VerifyOtp()
    {

    }

    public override string ToString()
    {
        return base.ToString();
    }

    
}


public class ClientLogin : AuthenticationManager
{
    Client client;

    public ClientLogin()
    {
        client = new Client();
    }

    public override void LoginOnClick(string mail, string password)
    {
        Task _ = client.ClientLogin(Utility.ClientLoginMailUrl, mail, password);
        Debug.Log($"{nameof(ClientLogin)} \t {nameof(LoginOnClick)}");
    }

    public override void ForgotPasswordOtpGenerator(string mail)
    {

    }

    public override void VerifyOtp()
    {

    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class EndUserLogin : AuthenticationManager
{
    EndUser endUser;

    public EndUserLogin()
    {
        endUser = new EndUser();
    }

    public override void LoginOnClick(string mail, string password)
    {
        Task _ = endUser.EndUserLogin(Utility.EndUserLoginMailUrl, mail, password);
        Debug.Log($"{nameof(EndUserLogin)} \t {nameof(LoginOnClick)}");
    }

    public override void ForgotPasswordOtpGenerator(string mail)
    {
        
    }

    public override void VerifyOtp()
    {

    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class AuthenticationManager : MonoBehaviour
{
    public virtual async void LoginOnClick(string mail, string password)
    {
        try
        {
            using (UnityWebRequest loginWebRequest = UnityWebRequest.Get($"{Utility.PayLaterLoginMailUrl}?mail={mail}&password={password}"))
            {
                loginWebRequest.SendWebRequest();
                await Task.Delay(3000);
                if (loginWebRequest.result == UnityWebRequest.Result.Success)
                {
                    PayLater payLaterLogin = JsonConvert.DeserializeObject<PayLater>(loginWebRequest.downloadHandler.text);
                    Debug.Log($"Vendor \n Response code {payLaterLogin.responseCode} \n message {payLaterLogin.message}");

                    // If login is successfull.
                    if (payLaterLogin.responseCode == "200")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{payLaterLogin.message}");

                        Authentication.OnMainPanel?.Invoke(1);
                        // Authentication.OnPanel?.Invoke(4);
                    }
                    // If already logged In
                    if (payLaterLogin.responseCode == "202")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{payLaterLogin.message}");

                        Authentication.OnMainPanel?.Invoke(1);
                    }
                    // Incorrect input details.
                    if (payLaterLogin.responseCode == "400")
                    {
                        // Display message
                        Authentication.OnAuthenticationMessage?.Invoke($"{payLaterLogin.message}");
                    }
                }
                else
                {
                    Authentication.OnAuthenticationMessage?.Invoke($"No details of the vendor foud. Sign up");

                }
                loginWebRequest.Dispose();

            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);

        }
        Debug.Log($"{nameof(AuthenticationManager)} \t {nameof(LoginOnClick)}");
        
    }

    public virtual void RegisterOnSubmit<T>(string mail, string password)
    {
        Debug.Log($"{nameof(AuthenticationManager)} \t {nameof(RegisterOnSubmit)}");
        
    }

    public virtual void ForgotPasswordOtpGenerator(string mail)
    {
        Debug.Log($"{nameof(AuthenticationManager)} \t {nameof(ForgotPasswordOtpGenerator)}");
    }

    public virtual void VerifyOtp()
    {
        Debug.Log($"{nameof(AuthenticationManager)} \t {nameof(VerifyOtp)}");

    }

    public virtual void CountDownTimer()
    {
        Debug.Log($"{nameof(AuthenticationManager)} \t {nameof(CountDownTimer)}");
    }
}
