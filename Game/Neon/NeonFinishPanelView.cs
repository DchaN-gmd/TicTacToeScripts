using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class NeonFinishPanelView : FinishPanelView
    {
        [Header("Win Images")]
        [SerializeField] private Image _winnerImage;
        [SerializeField] private Image _winImage;
        [SerializeField] private Image _drawImage;

        [Header("Animation Parameters")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private RectTransform _showPoint;
        [SerializeField] private float _speed;

        protected override void Show()
        {
            _panel.DOMove(_showPoint.position, _speed);
        }

        protected override void OnSettingDraw()
        {
            _winnerImage.gameObject.SetActive(false);
            _winImage.gameObject.SetActive(false);
            _drawImage.gameObject.SetActive(true);
        }
    }
}
