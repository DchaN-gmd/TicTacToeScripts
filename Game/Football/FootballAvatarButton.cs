using UnityEngine;
using UnityEngine.Events;

public class FootballAvatarButton : AvatarButton
{
    [SerializeField] private FootballData _data;
    [SerializeField] private UnityEvent<FootballAvatarButton> _customClicked;

    public event UnityAction<FootballAvatarButton> CustomClicked
    {
        add => _customClicked.AddListener(value);
        remove => _customClicked.RemoveListener(value);
    }

    public FootballData Data => _data;

    protected override void CustomOnClick()
    {
        _customClicked.Invoke(this);
    }
}
