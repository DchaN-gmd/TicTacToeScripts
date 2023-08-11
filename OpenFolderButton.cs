using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

[RequireComponent(typeof(Button))]
public class OpenFolderButton : MonoBehaviour
{
    [SerializeField] private string _musicFolderPath;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenFolder);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenFolder);
    }

    private void OpenFolder()
    {
        var path = Path.Combine(Application.streamingAssetsPath, _musicFolderPath);
        Application.OpenURL($"file://{path}");
    }
}
