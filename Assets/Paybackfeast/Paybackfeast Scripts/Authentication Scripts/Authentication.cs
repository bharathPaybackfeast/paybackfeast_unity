using TMPro;

using UnityEngine;
using UnityEngine.UI;


#if UNITY_2021_3_OR_NEWER
public class Authentication : MonoBehaviour
{


    public delegate void LoginDelegate();
    public static LoginDelegate OnResendPasswordOtpGeneratorButton;

    public delegate void MessageDelegate(string message);
    public static MessageDelegate OnAuthenticationMessage;

    public delegate void AuthButtonDelegate(byte index);
    public static AuthButtonDelegate OnPanelButton;

    public delegate void AuthPanelDelegate(byte index);
    public static AuthPanelDelegate OnPanel;

    public delegate void MainPanelDelegate(byte index);
    public static MainPanelDelegate OnMainPanel;



    #region Serialize private fields
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Button _loginSelectionButton;
    [SerializeField] private Button _registerSelectionButton;
    [SerializeField] private RectTransform _defaultAuthPanel;
    [SerializeField] private RectTransform[] _authPanels;

    [SerializeField] public RectTransform       _defaultMainPanel;
    [SerializeField] private RectTransform[]    _mainPanel;

    [SerializeField] private Button _defaultAuthPanelSelectionButton;
    [SerializeField] private Button[] _authPanelPanelSelectionButtons;
    #endregion

    #region Private fields
    private Notifier _notifier;
    private AndroidNotifications _androidNotifications;
    private AuthenticationManager _vendorLogin;
    private AuthenticationManager _clientLogin;
    private AuthenticationManager _endUserLogin;
    #endregion

    #region Public fields

    #endregion

    private void Awake()
    {
        _notifier = new Notifier();
       



#if UNITY_ANDROID
        _androidNotifications = new AndroidNotifications();
#endif
        _vendorLogin = new VendorAuth();
        _clientLogin = new ClientLogin();
        _endUserLogin = new EndUserLogin();

    }

    private void OnEnable()
    {
        // OnResendPasswordOtpGeneratorButton += ToggleResendPasswordOtpGeneratorButton;
        OnAuthenticationMessage += MessageOutPut;
        OnPanelButton += AuthPanelButtonClick;
        OnPanel += PanelChange;
        OnMainPanel += MainPanelChange;
    }

    private void OnDisable()
    {
        // OnResendPasswordOtpGeneratorButton -= ToggleResendPasswordOtpGeneratorButton;
        OnAuthenticationMessage -= MessageOutPut;
        OnPanelButton -= AuthPanelButtonClick;
        OnPanelButton -= PanelChange;
        OnMainPanel -= MainPanelChange;
    }

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID
        _androidNotifications.SetNotificationProperties();
        // _androidNotifications.SendNotification();
#endif
    }


    #region Private methods
    /// <summary>
    /// Generate the OTP on forgot password button click or resending the OTP on resend password button click.
    /// </summary>
    public void GenerateForgotPasswordOtpOnClick()
    {
        // OtpHandler.OnOtpHandler?.Invoke();
        OnResendPasswordOtpGeneratorButton?.Invoke();

    }

    
    #endregion

    /// <summary>
    /// Toggle resend password button.
    /// </summary>
    /*void ToggleResendPasswordOtpGeneratorButton()
    {
        //_resendForgotPasswordButton.enabled = !_resendForgotPasswordButton.enabled;
    }*/

    /// <summary>
    /// Display message
    /// </summary>
    /// <param name="message"></param>
    void MessageOutPut(string message)
    {
        _messageText.text = message;    
    }

    /// <summary>
    /// Invoke the events on button click
    /// </summary>
    /// <param name="index"></param>
    void AuthPanelButtonClick(byte index)
    {

        // Update the default panel selection button.
        _defaultAuthPanelSelectionButton = _authPanelPanelSelectionButtons[index];

        // Invoke all the events registered with this button.
        _defaultAuthPanelSelectionButton.onClick.Invoke();


        Debug.Log($"{nameof(AuthPanelButtonClick)} \t Panel index {index}");
    }

    /// <summary>
    /// Update Panel on button click
    /// </summary>
    /// <param name="index"></param>
    void PanelChange(byte index)
    {
        _defaultAuthPanel.gameObject.SetActive(false);
        _defaultAuthPanel = _authPanels[index];
        _defaultAuthPanel.gameObject.SetActive(true);
    }


    
    public void MainPanelChange(byte index)
    {
        _defaultMainPanel.gameObject.SetActive(false);
        _defaultMainPanel = _mainPanel[index];
        _defaultMainPanel.gameObject.SetActive(true);
    }


}
#endif
