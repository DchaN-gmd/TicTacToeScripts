using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SettingsPanel : MonoBehaviour
{
    private readonly int ShowKey = Animator.StringToHash("Show");
    private readonly int HideKey = Animator.StringToHash("Hide");

    [SerializeField] private Button _showButton;
    [SerializeField] private Button _hideButton;

    private Animator _animator;

    private bool _isShow;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _showButton.onClick.AddListener(Show);
        _hideButton.onClick.AddListener(Hide); 
    }

    private void OnDisable()
    {
        _showButton.onClick.RemoveListener(Show);
        _hideButton?.onClick.RemoveListener(Hide);
    }

    private void Start()
    {
        _isShow = false;
    }

    private void Show()
    {
        if (_isShow) return;
        _animator.SetTrigger(ShowKey);
        _isShow = true;
    }

    private void Hide()
    {
        if (!_isShow) return;
        _animator.SetTrigger(HideKey);
        _isShow = false;
    }
}
