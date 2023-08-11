using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Game;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [Header("Animation settings")]
    [SerializeField] private RectTransform _panel;
    [SerializeField] private RectTransform _showPanelPoint;
    [SerializeField] private RectTransform _hidePanelPoint;
    [SerializeField] private float _delay;

    [Header("RaycastPanel")]
    [SerializeField] private RectTransform _blockRaycast;
    [SerializeField] private RectTransform _defaultPosition;
    [SerializeField] private RectTransform _activePoistion;

    [Header("Buttons")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _finishButton;
    [SerializeField] private Button _exitButton;

    [Header("Load Scene Buttons")]
    [SerializeField] private List<LoadSceneButton> _loadSceneButtons;

    [Header("Events")]
    [SerializeField] private UnityEvent _callingFinish;

    public event UnityAction CallingFinish
    {
        add => _callingFinish.AddListener(value);
        remove => _callingFinish.RemoveListener(value);
    }

    private void Awake()
    {
        Time.timeScale = 1;
        
        foreach (var button in _loadSceneButtons)
        {
            if(button.SceneName == SceneManager.GetActiveScene().name) button.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(Hide);
        _finishButton.onClick.AddListener(CallFinish);
        _exitButton.onClick.AddListener(ResetTime);

        if(PauseMode.IsShowMode == false) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(Hide);
        _finishButton.onClick.RemoveListener(CallFinish);
        _exitButton.onClick.RemoveListener(ResetTime);
    }

    public void Show()
    {
        Time.timeScale = 0;
        _panel.DOMove(_showPanelPoint.position, _delay).SetUpdate(true).OnComplete(()=> _blockRaycast.DOMove(_activePoistion.position, _delay).SetUpdate(true));
    }

    private void Hide()
    {
        Time.timeScale = 1;
        _panel.DOMove(_hidePanelPoint.position, _delay);
        _blockRaycast.DOMove(_defaultPosition.position, _delay);
    }

    private void CallFinish()
    {
        Hide();
        _callingFinish?.Invoke();
    }

    private void ResetTime()
    {
        Time.timeScale = 1;
    }
}
