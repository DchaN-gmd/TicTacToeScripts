using TMPro;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (20.06.2022)
namespace License
{
    public class NotificationWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _notificationText;

        private string _defaultNotificationText;

        public void Initialize(int validityPeriodCount)
        {
            if (string.IsNullOrEmpty(_defaultNotificationText))
                _defaultNotificationText = _notificationText.text;
            _notificationText.text = $"{_defaultNotificationText} {validityPeriodCount}";
        }
        
        public void Show() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}
