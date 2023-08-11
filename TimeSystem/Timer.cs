using System;
using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems
{
    [AddComponentMenu("Time systems/Time systems/Timer")]
    public class Timer : MonoBehaviour, IPlayable, IStoppable, IPausable, IUpdateable
    {
        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private bool _playOnAwake;
        [SerializeField] [Min(0)] private float _startSeconds;

        [Header("Events")]
        [SerializeField] private UnityEvent _played;
        [SerializeField] private UnityEvent _stopped;
        [SerializeField] private UnityEvent<bool> _paused;
        [SerializeField] private UnityEvent<float> _updated;
        #endregion

        #region Fields
        private float _secondsLeft;
        private float _deltaTime;
        private float _deltaPauseTime;
        #endregion

        #region Properties
        public bool IsPlay { get; private set; }
        public bool IsPause { get; private set; }

        public float StartSeconds
        {
            get => _startSeconds;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException($"Start seconds can't be less than 0; Start seconds = {StartSeconds}");
                _startSeconds = value;
            }
        }
        
        public float SecondsLeft
        {
            get => _secondsLeft;
            private set => _secondsLeft = value < 0 ? 0 : value;
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
            if (_playOnAwake) Play(_startSeconds);
        }

        private void Update()
        {
            if (!IsPlay || IsPause) return;
            
            SecondsLeft = _startSeconds - (Time.time - _deltaTime);
            _updated?.Invoke(SecondsLeft);
            if (SecondsLeft == 0) Stop();
        }
        #endregion

        public void Play(float seconds)
        {
            if (IsPlay)
            {
                Debug.LogWarning("The timer is already running");
                return;
            }
        
            StartSeconds = seconds;
            SecondsLeft = seconds;
            _deltaTime = Time.time;
            IsPlay = true;
        
            _played?.Invoke();
        }

        public void Stop()
        {
            if (!IsPlay)
            {
                Debug.LogWarning("The timer has already been stopped");
                return;
            }
            
            SecondsLeft = 0;
            IsPlay = false;

            _stopped?.Invoke();
        }

        public void SetPause(bool value)
        {
            if (IsPause == value)
            {
                Debug.LogWarning(value ? "The timer is already on pause" : "The timer is already running");
                return;
            }
            
            IsPause = value;
            if (value) _deltaPauseTime = Time.time;
            else _deltaTime += Time.time - _deltaPauseTime;
            
            _paused?.Invoke(value);
        }
    }
}
