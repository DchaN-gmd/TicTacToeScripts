using TMPro;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (20.06.2022)
namespace License
{
    public class ComputerInfoWindow : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _cpuName;
        [SerializeField] private TextMeshProUGUI _gpuName;
        [SerializeField] private TextMeshProUGUI _computerName;
        [SerializeField] private TextMeshProUGUI _ramNumber;

        private void Start()
        {
            _cpuName.text += SystemInfo.processorType;
            _gpuName.text += SystemInfo.graphicsDeviceName;
            _computerName.text += SystemInfo.deviceName;
            _ramNumber.text += $"{SystemInfo.systemMemorySize / 1024} GB";
        }
    }
}
