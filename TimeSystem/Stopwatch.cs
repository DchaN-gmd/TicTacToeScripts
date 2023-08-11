using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems
{
    [AddComponentMenu("Time systems/Time systems/Stopwatch")]
    public sealed class Stopwatch : MonoBehaviour, IPlayable, IStoppable, IPausable, IUpdateable
    {
        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private bool _playOnAwake;

        [Header("Events")]
        [SerializeField] private UnityEvent _played;
        [SerializeField] private UnityEvent _stopped;
        [SerializeField] private UnityEvent<bool> _paused;
        [SerializeField] private UnityEvent<float> _updated;
        #endregion

        #region Fields
        private float _seconds;
        private float _deltaTime;
        private float _deltaPauseTime;
        #endregion

        #region Properties
        public bool IsPlay { get; private set; }
        public bool IsPause { get; private set; }
    
        public float Seconds
        {
            get => _seconds;
            private set => _seconds = value < 0 ? 0 : value;
        }
        #endregion

        #region Events
        public event UnityAction Played
        {
            add => _played.AddListener(value);
            remove => _played.RemoveListener(value);
        }
    
        public event UnityAction Stopped
        {
            add => _stopped.AddListener(value);
            remove => _stopped.RemoveListener(value);
        }
    
        public event UnityAction<bool> Paused
        {
            add => _paused.AddListener(value);
            remove => _paused.RemoveListener(value);
        }
    
        public event UnityAction<float> Updated
        {
            add => _updated.AddListener(value);
            remove => _updated.RemoveListener(value);
        }
        #endregion

        #region Unity functions
        private void Awake()
        {
            if (_playOnAwake) Play();
        }

        private void Update()
        {
            if (!IsPlay || IsPause) return;
            
            Seconds = Time.time - _deltaTime;
            _updated?.Invoke(Seconds);
        }
        #endregion

        public void Play()
        {
            if (IsPlay)
            {
                Debug.LogWarning("The stopwatch is already running");
                return;
            }
        
            _deltaTime = Time.time;
            IsPlay = true;
        
            _played?.Invoke();
        }

        public void Stop()
        {
            if (!IsPlay)
            {
                Debug.LogWarning("The stopwatch has already been stopped");
                return;
            }
        
            IsPlay = false;

            _stopped?.Invoke();
            Seconds = 0;
        }

        public void SetPause(bool value)
        {
            if (IsPause == value)
            {
                Debug.LogWarning(value ? "The stopwatch is already on pause" : "The stopwatch is already running");
                return;
            }
            
            IsPause = value;
            if (value) _deltaPauseTime = Time.time;
            else _deltaTime += Time.time - _deltaPauseTime;
            
            _paused?.Invoke(value);
        }
    }
}
