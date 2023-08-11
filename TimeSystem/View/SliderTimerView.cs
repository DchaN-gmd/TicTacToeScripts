using UnityEngine;
using UnityEngine.UI;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems.View
{
    [AddComponentMenu("Time systems/Views/Slider timer view")]
    public sealed class SliderTimerView : TimeView<Timer>
    {
        [SerializeField] private Slider _slider;

        #region Unity functions
        protected override void OnEnable()
        {
            base.OnEnable();
            TimeSystem.Played += Initialize;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            TimeSystem.Played -= Initialize;
        }
        #endregion

        protected override void UpdateTime(float time) => _slider.value = time;
        
        private void Initialize()
        {
            _slider.maxValue = TimeSystem.StartSeconds;
            _slider.value = _slider.maxValue;
        }
    }
}
