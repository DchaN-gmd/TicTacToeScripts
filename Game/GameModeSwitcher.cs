using UnityEngine;
using UnityEngine.UI;

public class GameModeSwitcher : MonoBehaviour
{
    private readonly string TapeModeKey = "TapeMode";

    [SerializeField] private Toggle _toggle;

    public static bool IsCustomMode;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(TapeModeKey) == 1) IsCustomMode = false;
        else IsCustomMode = true;

        _toggle.isOn = !IsCustomMode;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ChangeMode);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ChangeMode);
    }

    private void ChangeMode(bool toggleValue)
    {
        if (!toggleValue)
        {
            PlayerPrefs.SetInt(TapeModeKey, 0);
            IsCustomMode = true;
        }
        else
        {
            PlayerPrefs.SetInt(TapeModeKey, 1);
            IsCustomMode = false;
        }
    }
}
