using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DevXUnity.SerialNumberLicense.Tools;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (20.06.2022)
namespace License
{
    public class LicenseWindow : MonoBehaviour
    {
        private enum StateLicenseScene
        {
            FirstStart,
            NoFirstStart,
        }
        
        private const string LicenseKey = nameof(LicenseKey);
        private static StateLicenseScene _stateLicenseScene = StateLicenseScene.FirstStart;

        #region Inspector fields
        [SerializeField] private GameObject _loadingScreen;

        [Header("Notification")]
        [SerializeField] [Min(0)] private int _daysBeforeNotification;
        [SerializeField] [Min(0)] private float _timeNotification;
        [SerializeField] private NotificationWindow _notification;
        
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _date;
        [SerializeField] private TextMeshProUGUI _personalCode;
        [SerializeField] private TextMeshProUGUI _validityPeriod;

        [Space]
        [Header("Buttons")]
        [SerializeField] private Button _pasteButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_InputField _inputField;
        #endregion

        #region Fields
        private bool _isValidate;
        private int? _validityPeriodCount;
        private string _defaultValidityPeriodText;
        #endregion

        #region Unity functions
        private void OnEnable()
        {
            _pasteButton.onClick.AddListener(VerifyKeyFromInputField);
            _startButton.onClick.AddListener(StartGame);
            _inputField.onEndEdit.AddListener(Verify);
        }

        private void OnDisable()
        {
            _pasteButton.onClick.RemoveListener(VerifyKeyFromInputField);
            _startButton.onClick.RemoveListener(StartGame);
            _inputField.onEndEdit.RemoveListener(Verify);
        }
    
        private void Start()
        {
            Initialize();
            
            Verify(PlayerPrefs.GetString(LicenseKey, ""));
            if (_stateLicenseScene == StateLicenseScene.FirstStart)
            {
                _stateLicenseScene = StateLicenseScene.NoFirstStart;
                if (_isValidate)
                {
                    if (_validityPeriodCount <= _daysBeforeNotification)
                    {
                        ShowNotification();
                        Invoke(nameof(StartGame), _timeNotification);
                    }
                    else StartGame();
                }
                else DeactivateLoadingScreen();
            }
            else DeactivateLoadingScreen();
        }
        #endregion

        private void Initialize()
        {
            _startButton.interactable = false;
            _defaultValidityPeriodText = _validityPeriod.text;
            var key = PlayerPrefs.GetString(LicenseKey, "");
        
            _inputField.text = key;
            _date.text = DateTime.Today.ToString("dd.MM.yy");
            _personalCode.text = SerialNumberValidateTools.HardwareID;
        }

        #region Verify
        private void VerifyKeyFromInputField() =>
            Verify(_inputField.text);

        private void Verify(string key)
        {
            SerialNumberValidateTools.SerialNumberKey = key;
            _isValidate = SerialNumberValidateTools.Verify(out _validityPeriodCount);

            _validityPeriodCount ??= 0;
            _startButton.interactable = _isValidate;
            _validityPeriod.text = $"{_defaultValidityPeriodText} {_validityPeriodCount}";

            if (_isValidate) PlayerPrefs.SetString(LicenseKey, key);
        }
        #endregion

        private void ShowNotification()
        {
            _validityPeriodCount ??= 0;
            _notification.Initialize(_validityPeriodCount.Value);
            _notification.Show();
        }
        
        private void StartGame() =>
            SceneManager.LoadScene(sceneBuildIndex: 1);
        
        private void DeactivateLoadingScreen() =>
            _loadingScreen.SetActive(false);
    }
}
