using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Authentication;

public class Register : MonoBehaviour
{

    /// <summary>
    /// Mail.
    /// </summary>
    [SerializeField] private TMP_InputField _mail;

    /// <summary>
    /// password
    /// </summary>
    [SerializeField] private TMP_InputField _password;

    /// <summary>
    /// Re Enter  Password.
    /// </summary>
    [SerializeField] private TMP_InputField _reEnterPassword;

    /// <summary>
    /// common button for login and registeration.
    /// </summary>
    [SerializeField] Button _registerButton;

    private AuthenticationManager _vendor;
    private AuthenticationManager _client;
    private AuthenticationManager _endUser;

    #region Monobehaviour callbacks

    private void Awake()
    {
        _vendor = new VendorAuth();
        _client = new ClientLogin();
        _endUser = new EndUserLogin();
    }

    // Start is called before the first frame update
    void Start()
    {
        _registerButton.onClick.AddListener(() => RegisterOnClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Private methods
    void RegisterOnClick()
    {
        var message = "";
        _mail.text.Trim();
        _password.text.Trim();
        _reEnterPassword.text.Trim();
        if (_mail.text == string.Empty)
        {
            message = ("mail can't be empty");
        }
        if (_password.text == string.Empty)
        {
            message = ("Password can't be empty");
        }
        if(_reEnterPassword.text == string.Empty) {
            message = ("Re Enter Password can't be empty");
        }
        if (_mail.text == string.Empty && _password.text == string.Empty && _reEnterPassword.text == string.Empty)
        {
            message = ("All fields can't be empty.");
        }
        else if (_mail.text != string.Empty && _password.text != string.Empty && _reEnterPassword.text != string.Empty)
        {
            if (_password.text == _reEnterPassword.text)
            {
                _vendor.RegisterOnSubmit<Vendor>(_mail.text, _password.text);
                message = ("");
            }
            else
            {
                message = ("Password missmatch, try again");
            }
        }
        Authentication.OnAuthenticationMessage?.Invoke(message);
        Debug.Log($"{nameof(Login)} \t {nameof(RegisterOnClick)}");
    }
    #endregion


}
