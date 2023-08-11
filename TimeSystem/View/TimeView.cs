using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems.View
{
    public abstract class TimeView<TUpdateable> : MonoBehaviour where TUpdateable : Component, IUpdateable
    {
        [Header("Controllers")]
        [SerializeField] private TUpdateable _timeSystem;
        
        protected TUpdateable TimeSystem => _timeSystem;
        
        #region Unity functions
        protected virtual void OnEnable() => _timeSystem.Updated += UpdateTime;
        protected virtual void OnDisable() => _timeSystem.Updated -= UpdateTime;
        #endregion
        
        protected abstract void UpdateTime(float time);
    }
}
