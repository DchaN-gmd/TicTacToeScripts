using System;
using UnityEngine;
using UnityEngine.UI;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems.View
{
    [AddComponentMenu("Time systems/Views/Image timer view")]
    public sealed class ImageTimerView : TimeView<Timer>
    {
        [SerializeField] private Image _image;

        #region Unity functions
        private void OnValidate()
        {
            if (_image.type == Image.Type.Simple)
                throw new Exception("The image cannot be simple");
        }

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

        protected override void UpdateTime(float time) =>
            _image.fillAmount = time / TimeSystem.StartSeconds;
        
        private void Initialize()
        {
            if (_image.type == Image.Type.Simple)
                throw new Exception("The image cannot be simple");
                
            _image.fillAmount = 1;
        }
    }
}
