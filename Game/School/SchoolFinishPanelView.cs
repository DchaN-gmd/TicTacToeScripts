using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SchoolFinishPanelView : FinishPanelView
    {
        [Header("Win Images")]
        [SerializeField] private Image _winnerImage;

        [Header("Text")]
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private string _drawText;

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
            _winText.text = _drawText;
        }
    }
}
