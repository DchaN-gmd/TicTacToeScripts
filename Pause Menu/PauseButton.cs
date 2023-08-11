using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using TimeSystems;

namespace Pause
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Timer))]
    public class PauseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [Header("Reference")]
        [SerializeField] private Timer _timer;

        [SerializeField][Min(0)] private int _clickToActivateCount;
        [SerializeField] private float _timeToResetClicks;

        [Header("Fill")]
        [SerializeField] private float _fillSpeed;
        [SerializeField] private Image _fill;

        [Header("Events")]
        [SerializeField] private UnityEvent _activated;

        private Tween _fillTween;
        private int _clickCount = 0;

        public event UnityAction Activated
        {
            add => _activated.AddListener(value);
            remove => _activated.RemoveListener(value);
        }

        private void OnEnable()
        {
            _timer.Stopped += ResetClicks;
        }

        private void OnDisable()
        {
            _timer.Stopped -= ResetClicks;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Fill();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_fillTween != null) _fillTween.Kill();
            _fill.fillAmount = 0f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_clickCount == 0)
            {
                _timer.Play(_timeToResetClicks);
            }

            _clickCount++;

            if (_clickCount >= _clickToActivateCount)
            {
                _timer.Stop();
                _activated.Invoke();
            }
        }

        private void Fill()
        {
            _fillTween = _fill.DOFillAmount(1, _fillSpeed).OnComplete(() => _activated.Invoke());
        }

        private void ResetClicks()
        {
            _clickCount = 0;
        }
    }
}
