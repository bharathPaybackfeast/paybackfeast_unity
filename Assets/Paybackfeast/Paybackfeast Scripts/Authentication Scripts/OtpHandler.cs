using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class OtpHandler : MonoBehaviour
{

    public delegate void OtpHandlerDelegate();
    public static OtpHandlerDelegate OnOtpHandler;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _maxSeconds = 60f;

    float _remainingSeconds;
    bool  _enableTimer = false;


    #region Monobehaviour callbacks

    private void OnEnable()
    {
        OnOtpHandler += StartTimer;
    }

    void Start()
    {
        _remainingSeconds = _maxSeconds;
    }

    void Update()
    {
        if(!_enableTimer)
            return;

        CountDownTimerStart();
    }
    #endregion

    void StartTimer()
    {
        _enableTimer = true;
        _remainingSeconds = _maxSeconds;
    }


    void CountDownTimerStart()
    {
        if (_timerText == null)
            return;

        Debug.Log($"{nameof(CountDownTimerStart)}");

        
        _timerText.text = _remainingSeconds.ToString();

        if (_remainingSeconds >= 0)
        {
            _remainingSeconds -= Time.deltaTime;
            _timerText.text = MathF.Round(_remainingSeconds).ToString();
        }
        else if(_remainingSeconds <= 0)
        {
            _remainingSeconds = 0;
            _timerText.text = _remainingSeconds.ToString();
            _enableTimer = false;
            Authentication.OnResendPasswordOtpGeneratorButton?.Invoke();
        }


    }

}
