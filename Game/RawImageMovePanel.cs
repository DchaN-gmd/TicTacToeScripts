using Game;
using UnityEngine;
using UnityEngine.UI;

public class RawImageMovePanel : PlayerMovePanel
{
    [SerializeField] private RawImage _firstPlayerImage;
    [SerializeField] private RawImage _secondPlayerImage;

    public override void ActivateCurrentPlayerImage(Players player)
    {
        if (player == Players.First)
        {
            _firstPlayerImage.enabled = true;
            _secondPlayerImage.enabled = false;
        }
        else if (player == Players.Second)
        {
            _secondPlayerImage.enabled = true;
            _firstPlayerImage.enabled = false;
        }
    }
}
