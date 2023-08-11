using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems
{
    [AddComponentMenu("Time systems/Time systems/Time manipulator")]
    public sealed class TimeManipulator : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _onSetTimeScale;
        
        public event UnityAction<float> OnSetTimeScale
        {
            add => _onSetTimeScale.AddListener(value);
            remove => _onSetTimeScale.RemoveListener(value);
        }

        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            _onSetTimeScale?.Invoke(timeScale);
        }

        public void StopTime() => SetTimeScale(0);

        public void ResetTime() => SetTimeScale(1);
    }
}
