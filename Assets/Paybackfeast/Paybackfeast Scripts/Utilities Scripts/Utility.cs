using System.Net.NetworkInformation;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public enum Roles
    {
        Vendor = 1,
        Client,
        EndUser
    }



    public Roles Role;

    private static string _payLaterLoginMailUrl = "http://localhost:8080/api/paylater/login";

    private static string _vendorLoginMailUrl = "http://localhost:8080/api/vendor/login";

    private static string _vendorRegisterMailUrl = "http://localhost:8080/api/vendor/add";

    private static string _vendorForgotPasswordOtpUrl = "http://localhost:8080/api/vendor/forgot_password_otp";

    private static string _clientLoginMailUrl = "http://localhost:8080/api/client/login";
    
    private static string _endUserLoginMailUrl = "http://localhost:8080/api/enduser/login";
    
    public static string PayLaterLoginMailUrl { get { return _payLaterLoginMailUrl; } }
    public static string VendorLoginMailUrl { get { return _vendorLoginMailUrl; } private set { _vendorLoginMailUrl = value; } }
    public static string VendorRegisterMailUrl { get { return _vendorRegisterMailUrl; } private set { _vendorRegisterMailUrl = value; } }
    public static string VendorForgotPasswordOtpUrl { get { return _vendorForgotPasswordOtpUrl; } private set { _vendorForgotPasswordOtpUrl = value; } }
    public static string ClientLoginMailUrl { get { return _clientLoginMailUrl; } private set { _clientLoginMailUrl = value; } }
    public static string EndUserLoginMailUrl { get { return _endUserLoginMailUrl; } private set { _endUserLoginMailUrl = value; } }

}
