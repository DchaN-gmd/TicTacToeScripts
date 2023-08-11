using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NeonMovePanel : PlayerMovePanel
{
    [Header("Player Colors")]
    [SerializeField] private Color _firstPlayerColor;
    [SerializeField] private Color _secondPlayerColor;

    [Header("Parameters")]
    [SerializeField] private float _speed;

    [Header("Images")]
    [SerializeField] private Image _sharp;
    [SerializeField] private Image _moveBoard;
    [SerializeField] private Image _pauseButton;
    [SerializeField] private Image _fillPauseButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _moveText;
    [SerializeField] private string _firtPlayerText;
    [SerializeField] private string _secondPlayerText;

    public override void ActivateCurrentPlayerImage(Players player)
    {
        if(player == Players.First)
        {
            _sharp.DOColor(_firstPlayerColor, _speed);
            _moveBoard.DOColor(_firstPlayerColor, _speed);
            _moveText.text = _firtPlayerText;
            _moveText.DOColor(_firstPlayerColor, _speed);

            _pauseButton.DOColor(_firstPlayerColor, _speed);
            _fillPauseButton.DOColor(_firstPlayerColor, _speed);
        }
        else if(player == Players.Second)
        {
            _sharp.DOColor(_secondPlayerColor, _speed);
            _moveBoard.DOColor(_secondPlayerColor, _speed);
            _moveText.text = _secondPlayerText;
            _moveText.DOColor(_secondPlayerColor, _speed);

            _pauseButton.DOColor(_secondPlayerColor, _speed);
            _fillPauseButton.DOColor(_secondPlayerColor, _speed);
        }
    }
}
