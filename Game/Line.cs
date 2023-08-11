using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Events;

namespace Game
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private ShowType _showType;

        [SerializeField] private Image _image;
        [SerializeField] private Slider _slider;

        [SerializeField] private UnityEvent _showing;

        public event UnityAction Showing
        {
            add => _showing.AddListener(value);
            remove => _showing.RemoveListener(value);
        }
        
        public void Show(float speed)
        {
            if(_showType == ShowType.Image)
            {
                if (!_image) throw new NullReferenceException("Image has not been assignet");

                _image.DOFillAmount(1, speed);
            }
            else if(_showType == ShowType.Slider)
            {
                if (!_slider) throw new NullReferenceException("Slider has not been assignet");

                _slider.gameObject.SetActive(true);
                _slider.DOValue(1, speed);
            }

            _showing?.Invoke();
        }

        public void ResetLine()
        {
            if (_showType == ShowType.Image)
            {
                _image.fillAmount = 0;
            }
            else if (_showType == ShowType.Slider)
            {
                _slider.value = 0;
                _slider.gameObject.SetActive(false);
            }
        }
    }

    public enum ShowType
    {
        Image,
        Slider
    }
}
