using UnityEngine;
using UnityEngine.SceneManagement;

public class HotKeyToLoadScene : MonoBehaviour
{
    [SerializeField] private KeyCode _hotKey;
    [SerializeField] private string _sceneName;

    private bool _isActivated = false;

    private void Update()
    {
        if(Input.GetKey(_hotKey))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (_isActivated) return;

        _isActivated = true;
        SceneManager.LoadScene(_sceneName);
    }
}
