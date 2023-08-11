using UnityEngine;
using UnityEngine.SceneManagement;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.1 (20.06.2022)
namespace License
{
    public class ReturningToLicense : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (FindObjectsOfType<ReturningToLicense>().Length > 1)
                Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
                SceneManager.LoadScene(0);
        }
    }
}
