using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (20.06.2022)
namespace General
{
    public class SystemFunctions : MonoBehaviour
    {
        public static void Quit() => Application.Quit();

        public static void OpenURl(string url) => Application.OpenURL(url);
    }
}
