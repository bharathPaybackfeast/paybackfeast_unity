using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    /// <summary>
    /// Mail.
    /// </summary>
    [SerializeField] private TMP_InputField _mail;
    /// <summary>
    /// Password.
    /// </summary>
    [SerializeField] private TMP_InputField _password;
    /// <summary>
    /// login button.
    /// </summary>
    [SerializeField] Button _loginButton;
    /// <summary>
    /// Forgot password
    /// </summary>
    [SerializeField] Button _forgotPasswordButton;


    private AuthenticationManager _authenticationManager;
    private AuthenticationManager _vendorAuth;
    private AuthenticationManager _clientLogin;
    private AuthenticationManager _endUserLogin;

    #region


    private void Awake()
    {
        _authenticationManager = new AuthenticationManager();
        _vendorAuth = new VendorAuth();
        _clientLogin = new ClientLogin();
        _endUserLogin = new EndUserLogin();

        _forgotPasswordButton.onClick.AddListener(ForgotPasswordOnClick);
    }


    // Start is called before the first frame update
    void Start()
    {
        _loginButton.onClick.AddListener(() => LoginOnClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    void LoginOnClick()
    {
        var message = "";
        if(_mail.text == string.Empty)
        {
            message = ("mail can't be empty");
        }
        if( _password.text == string.Empty)
        {
            message = ("Password can't be empty");
        }
        if (_mail.text == string.Empty && _password.text == string.Empty)
        {
            message = ("All fields can't be empty.");
        }
        else if(_mail.text != string.Empty && _password.text != string.Empty)
        {
            // _vendorAuth.LoginOnClick(_mail.text, _password.text);
            
            _authenticationManager.LoginOnClick(_mail.text, _password.text);
            
            Debug.Log($"{nameof(Login)} \t {nameof(LoginOnClick)}");
        }
        Authentication.OnAuthenticationMessage?.Invoke(message);
        
    }

    void ForgotPasswordOnClick()
    {
        OtpHandler.OnOtpHandler?.Invoke();
    }

    
}
