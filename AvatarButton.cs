using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public abstract class AvatarButton : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Color _selectedColor;

    [Header("Events")]
    [SerializeField] private UnityEvent<AvatarButton> _clicked;

    private Button _button;
    private Image _image;
    private Color _normalColor;

    private bool _isSelected;

    public Image Image => _image;

    public event UnityAction<AvatarButton> Clicked
    {
        add => _clicked.AddListener(value);
        remove => _clicked.RemoveListener(value);
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _normalColor = _image.color;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public virtual void ResetButton()
    {
        _isSelected = false;
        _image.color = _normalColor;
    }

    public void SetUnclickingMode()
    {
        _isSelected = true;
    }

    private void OnClick()
    {
        if (_isSelected) return;

        _image.color = _selectedColor;
        _clicked?.Invoke(this);
        _isSelected = true;

        CustomOnClick();
    }

    protected abstract void CustomOnClick();
}
