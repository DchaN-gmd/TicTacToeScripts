using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using TMPro;

namespace Game
{
    public class FinishPanel : MonoBehaviour
    {
        [SerializeField] private bool _isCustomImagesMode;

        [Header("Reference")]
        [SerializeField] private Image _winnerImage;
        [SerializeField] private ScoreTable _scoreTable;
        [SerializeField] private TMP_Text _winnerText;
        [SerializeField] private RawImage _winnerRawImage;
        [SerializeField] private Button _restartButton;

        [Header("Parameters")]
        [SerializeField] private bool _isDelayRestart;
        [SerializeField][Min(0)]  private float _delayBeforeRestart;
        [SerializeField] protected Sprite _firstPlayerImage;
        [SerializeField] protected Sprite _secondPlayerImage;

        [Header("First Player")]
        [SerializeField] private Image _firstPlayerFinishImage;
        [SerializeField] private RawImage _firstPlayerFinishRawImage;
        [SerializeField] private TMP_Text _firstPlayerScore;

        [Header("Seecond Player")]
        [SerializeField] private Image _secondPlayerFinishImage;
        [SerializeField] private RawImage _secondPlayerFinishRawImage;
        [SerializeField] private TMP_Text _secondPlayerScore;


        [SerializeField] protected UnityEvent _showing;
        [SerializeField] protected UnityEvent _settingDraw;

        protected Texture2D _firstPlayerTexture;
        protected Texture2D _secondPlayerTexture;

        private Animator _animator;
        private Coroutine _delayRestart;

        public Players Winner { get; private set; }

        public event UnityAction Showing
        {
            add => _showing.AddListener(value);
            remove => _showing.RemoveListener(value);
        }

        public event UnityAction SettingDraw
        {
            add => _settingDraw.AddListener(value);
            remove => _settingDraw.RemoveListener(value);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            if(!_isDelayRestart) _restartButton.onClick.AddListener(Restart);
            else _restartButton.onClick.AddListener(DelayRestart);

        }

        private void OnDisable()
        {
            if (!_isDelayRestart) _restartButton.onClick.RemoveListener(Restart);
            else _restartButton.onClick.RemoveListener(DelayRestart);
        }

        public virtual void Show()
        {
            SetImagesAndScore();
            _showing?.Invoke();
        }

        public void SetCustomImages(Sprite _firstPlayerCustomImage, Sprite _secondPlayerCustomImage)
        {
            _firstPlayerImage = _firstPlayerCustomImage;
            _secondPlayerImage = _secondPlayerCustomImage;
        }

        public void SetCustomImages(Texture2D _firstPlayerCustomImage, Texture2D _secondPlayerCustomImage)
        {
            _firstPlayerTexture = _firstPlayerCustomImage;
            _secondPlayerTexture = _secondPlayerCustomImage;
        }

        public void SetWinner(Players player)
        {
            if(!_isCustomImagesMode)
            {
                if (player == Players.First) _winnerImage.sprite = _firstPlayerImage;
                else if (player == Players.Second) _winnerImage.sprite = _secondPlayerImage;
            }
            else
            {
                _winnerRawImage.enabled = true;

                if (player == Players.First) _winnerRawImage.texture = _firstPlayerTexture;
                else if (player == Players.Second) _winnerRawImage.texture = _secondPlayerTexture;
            }

            Winner = player;
        }

        public void SetDraw()
        {
            _settingDraw.Invoke();
        }

        private void DelayRestart()
        {
            if (_delayRestart != null) return;

            _delayRestart = StartCoroutine(DelaingRestart());
        }

        private void SetImagesAndScore()
        {
            if (!_isCustomImagesMode)
            {
                _firstPlayerFinishImage.sprite = _firstPlayerImage;
                _secondPlayerFinishImage.sprite = _secondPlayerImage;
            }
            else
            {
                _firstPlayerFinishRawImage.texture = _firstPlayerTexture;
                _secondPlayerFinishRawImage.texture = _secondPlayerTexture;
            }

            _firstPlayerScore.text = _scoreTable.FirstPlayerScore.ToString();
            _secondPlayerScore.text = _scoreTable.SecondPlayerScore.ToString();
        }

        private IEnumerator DelaingRestart()
        {
            yield return new WaitForSeconds(_delayBeforeRestart);
            Restart();
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
