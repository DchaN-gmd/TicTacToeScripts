using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TimeSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FinishButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Fill")]
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Image _fill;

    [Header("Events")]
    [SerializeField] private UnityEvent _activated;

    private Tween _fillTween;

    public event UnityAction Activated
    {
        add => _activated.AddListener(value);
        remove => _activated.RemoveListener(value);
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

    private void Fill()
    {
        _fillTween = _fill.DOFillAmount(1, _fillSpeed).OnComplete(() =>
        {
            _activated.Invoke();
            _fillTween.Kill();
            _fill.fillAmount = 0f;
        });
    }
}
