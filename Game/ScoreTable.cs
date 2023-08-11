using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image _firstPlayerImage;
    [SerializeField] private Image _secondPlayerImage;

    [Header("Raw Images")]
    [SerializeField] private RawImage _firstPlayerRawImage;
    [SerializeField] private RawImage _secondPlayerRawImage;

    [Header("Score Text")]
    [SerializeField] private TMP_Text _firstPlayerText;
    [SerializeField] private TMP_Text _secondPlayerText;

    private int _firstPlayerScore = 0;
    private int _secondPlayerScore = 0;

    public int FirstPlayerScore => _firstPlayerScore;
    public int SecondPlayerScore => _secondPlayerScore;

    public void SetImages(Sprite firstPlayer, Sprite secondPlayer)
    {
        _firstPlayerImage.sprite = firstPlayer;
        _secondPlayerImage.sprite = secondPlayer;
    }

    public void SetImages(Texture2D firstPlayer, Texture2D secondPlayer)
    {
        _firstPlayerRawImage.texture = firstPlayer;
        _secondPlayerRawImage.texture = secondPlayer;
    }

    public void UpPlayerScore(Players player)
    {
        if(player == Players.First) 
        {
            _firstPlayerScore++;
            _firstPlayerText.text = _firstPlayerScore.ToString();
        }
        else if(player == Players.Second)
        {
            _secondPlayerScore++;
            _secondPlayerText.text = _secondPlayerScore.ToString();
        }
    }


}
