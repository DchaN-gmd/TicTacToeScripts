using Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChooseAvatarPanel<T> : MonoBehaviour where T : AvatarButton
{
    #region Inspector Fields
    [Header("Avatars")]
    [SerializeField] private Image _firstPlayerAvatar;
    [SerializeField] private Image _secondPlayerAvatar;

    [Header("Reference")]
    [SerializeField] private FinishPanel _winPanel;
    [SerializeField] private ScoreTable _scoreTable;

    [SerializeField] protected TMP_Text _chooseText;
    [SerializeField] protected List<T> _avatarButtons;
    [SerializeField] protected List<Image> _firstPlayerImages;
    [SerializeField] protected List<Image> _secondPlayerImages;

    [Header("Buttons")]
    [SerializeField] protected Button _resetButton;
    [SerializeField] protected Button _startButton;

    [Header("Parameters")]
    [SerializeField] private string _firstPlayerChooseText;
    [SerializeField] private string _secondPlayerChooseText;
    #endregion

    #region Fields
    protected Image _firstPlayerImage;
    protected Image _secondPlayerImage;
    #endregion

    #region Unity Functions
    protected virtual void OnEnable()
    {
        foreach (var avatar in _avatarButtons)
        {
            avatar.Clicked += OnAvatarChose;
        }

        _resetButton.onClick.AddListener(ResetAvatars);
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    protected virtual void OnDisable()
    {
        foreach (var avatar in _avatarButtons)
        {
            avatar.Clicked -= OnAvatarChose;
        }

        _resetButton.onClick.RemoveListener(ResetAvatars);
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void Start()
    {
        SetChoiceText();
    }
    #endregion

    private void SetChoiceText()
    {
        if (!_firstPlayerImage) _chooseText.text = _firstPlayerChooseText;
        else if (!_secondPlayerImage) _chooseText.text = _secondPlayerChooseText;
    }

    protected virtual void OnAvatarChose(AvatarButton avatarButton)
    {
        if (!_firstPlayerImage)
        {
            _firstPlayerImage = avatarButton.Image;
            _firstPlayerAvatar.sprite = avatarButton.Image.sprite;

            _firstPlayerAvatar.enabled = true;
        }
        else if(!_secondPlayerImage)
        {
            _secondPlayerImage = avatarButton.Image;
            _secondPlayerAvatar.sprite = avatarButton.Image.sprite;

            _secondPlayerAvatar.enabled = true;
        }

        if(_firstPlayerImage && _secondPlayerImage)
        {
            _startButton.gameObject.SetActive(true);

            foreach(var button in _avatarButtons)
            {
                button.SetUnclickingMode();
            }

            return;
        }

        SetChoiceText();
    }

    protected virtual void ResetAvatars()
    {
        _firstPlayerImage = null;
        _secondPlayerImage = null;

        _firstPlayerAvatar.enabled = false;
        _secondPlayerAvatar.enabled = false;

        foreach(var avatarButton in _avatarButtons)
        {
            avatarButton.ResetButton();
        }

        _startButton.gameObject.SetActive(false);
        CustomReset();
        SetChoiceText();
    }

    protected virtual void OnStartButtonClicked()
    {
        foreach (var firstPlayerImage in _firstPlayerImages)
        {
            firstPlayerImage.sprite = _firstPlayerImage.sprite;
        }

        foreach (var secondPlayerImage in _secondPlayerImages)
        {
            secondPlayerImage.sprite = _secondPlayerImage.sprite;
        }

        _winPanel.SetCustomImages(_firstPlayerImage.sprite, _secondPlayerImage.sprite);
        _scoreTable.SetImages(_firstPlayerImage.sprite, _secondPlayerImage.sprite);
    }

    protected abstract void CustomOnClick(T avatarButton);
    protected abstract void CustomReset();
}
