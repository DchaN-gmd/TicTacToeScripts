using UnityEngine;
using UnityEngine.UI;

public class PauseMode : MonoBehaviour
{
    private readonly string PauseModeKey = "PauseMode";

    [SerializeField] private Toggle _toggle;

    public static bool IsShowMode;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(PauseModeKey) == 1) IsShowMode = true;
        else IsShowMode = false;

        _toggle.isOn = IsShowMode;
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
            PlayerPrefs.SetInt(PauseModeKey, 0);
            IsShowMode = false;
        }
        else
        {
            PlayerPrefs.SetInt(PauseModeKey, 1);
            IsShowMode = true;
        }
    }
}
