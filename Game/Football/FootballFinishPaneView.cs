using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Game
{
    public class FootballFinishPaneView : FinishPanelView
    {
        [SerializeField] private FootballMovePanel _movePanel;

        [Header("Animation Parameters")]
        [SerializeField] private RectTransform _panel;
        [SerializeField] private RectTransform _showPoint;
        [SerializeField] private float _speed;

        [Header("Text")]
        [SerializeField] private TMP_Text _winnerName;
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private string _drawText;

        [SerializeField] private Image _winnerImage;
        
        protected override void Show()
        {
            _panel.DOMove(_showPoint.position, _speed);
            SetWinnerTeamName();
        }

        public void SetWinnerTeamName()
        {
            _winnerName.text = _movePanel.GetTeamName(_finishPanel.Winner);
        }

        protected override void OnSettingDraw()
        {
            _winText.text = _drawText;
            _winnerImage.gameObject.SetActive(false);
            _winnerName.text = "";
        }
    }
}
