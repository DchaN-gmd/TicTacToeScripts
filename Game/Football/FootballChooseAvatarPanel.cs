using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FootballChooseAvatarPanel : ChooseAvatarPanel<FootballAvatarButton>
{
    [Header("Images")]
    [SerializeField] private Image _firstTeamPlayerImage;
    [SerializeField] private Image _secondTeamPlayerImage;

    [Header("Text")]
    [SerializeField] private TMP_Text _firstTeamName;
    [SerializeField] private TMP_Text _secondTeamName;

    [Header("Reference")]
    [SerializeField] private FootballMovePanel _movePanel;

    protected Players _currentPlayer = Players.First;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var avatar in _avatarButtons)
        {
            avatar.CustomClicked += CustomOnClick;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var avatar in _avatarButtons)
        {
            avatar.CustomClicked -= CustomOnClick;
        }
    }

    protected override void CustomOnClick(FootballAvatarButton avatarButton)
    {
        if (_currentPlayer == Game.Players.First)
        {
            _firstTeamPlayerImage.sprite = avatarButton.Data.TeamPlayer;
            _movePanel.SetTeamName(avatarButton.Data.TeamName, Game.Players.First);
            _firstTeamName.text = avatarButton.Data.FullTeamName;

            _currentPlayer = Players.Second;
        }
        else if(_currentPlayer == Game.Players.Second)
        {
            _secondTeamPlayerImage.sprite = avatarButton.Data.TeamPlayer;
            _movePanel.SetTeamName(avatarButton.Data.TeamName, Game.Players.Second);
            _secondTeamName.text = avatarButton.Data.FullTeamName;
        }
    }

    protected override void CustomReset()
    {
        _currentPlayer = Players.First;
        _firstTeamName.text = "";
        _secondTeamName.text = "";
    }
}
