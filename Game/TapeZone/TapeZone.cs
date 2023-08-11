using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class TapeZone<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _delayBeforeTapeAgain;

        [Header("Image")]
        [SerializeField] private T _firstPlayerImage;
        [SerializeField] private T _secondPlayerImage;

        [Header("Events")]
        [SerializeField] private UnityEvent _taped;

        private Button _button;
        [SerializeField] private Players _currentActivePlayerOnZone;
        private bool _canTape = true;

        public Players CurrentActivePlayerOnZone => _currentActivePlayerOnZone;

        public event UnityAction Taped
        {
            add => _taped.AddListener(value);
            remove => _taped.RemoveListener(value);
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _currentActivePlayerOnZone = Players.Empty;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnTape);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnTape);
        }

        public void ResetZone()
        {
            _secondPlayerImage.gameObject.SetActive(false);
            _firstPlayerImage.gameObject.SetActive(false);
            _currentActivePlayerOnZone = Players.Empty;

            _canTape = true;
        }

        private void OnTape()
        {
            if (!_canTape) return;

            if (_currentActivePlayerOnZone != Players.Empty && !GameModeSwitcher.IsCustomMode) return;

            if (_currentActivePlayerOnZone == GameController<T>.CurrentPlayer) return;

            if(GameController<T>.CurrentPlayer == Players.First)
            {
                _firstPlayerImage.gameObject.SetActive(true);
                _secondPlayerImage.gameObject.SetActive(false);

                _currentActivePlayerOnZone = Players.First;
            }
            if (GameController<T>.CurrentPlayer == Players.Second)
            {
                _secondPlayerImage.gameObject.SetActive(true);
                _firstPlayerImage.gameObject.SetActive(false);

                _currentActivePlayerOnZone = Players.Second;
            }

            _taped?.Invoke();
            _canTape = false;
            StartCoroutine(WaitingBeforeTapeAgain());
        }

        private IEnumerator WaitingBeforeTapeAgain()
        {
            yield return new WaitForSeconds(_delayBeforeTapeAgain);
            _canTape = true;
        }
    }
}
