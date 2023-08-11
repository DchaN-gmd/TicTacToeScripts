using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (14.07.2022)
namespace TimeSystems
{
    public interface IPausable
    {
        public event UnityAction<bool> Paused;
    }
}
