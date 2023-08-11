using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FootballMovePanel : PlayerMovePanel
{
    [SerializeField] private Image _firstTeamPlayerImage;
    [SerializeField] private Image _secondTeamPlayerImage;
    [SerializeField] private TMP_Text _teamNameText;

    private string _firstTeamName;
    private string _secondTeamName;

    private void Awake()
    {
        _firstTeamPlayerImage.enabled = false;
        _secondTeamPlayerImage.enabled = false;
    }

    public override void ActivateCurrentPlayerImage(Players player)
    {
        if(player == Players.First)
        {
            _firstTeamPlayerImage.enabled = true;
            _secondTeamPlayerImage.enabled = false;
            _teamNameText.text = _firstTeamName;
        }
        else if(player == Players.Second)
        {
            _firstTeamPlayerImage.enabled = false;
            _secondTeamPlayerImage.enabled = true;
            _teamNameText.text = _secondTeamName;
        }
    }

    public string GetTeamName(Players player)
    {
        if (player == Players.First) return _firstTeamName;
        else if (player == Players.Second) return _secondTeamName;

        return null;
    }

    public void SetTeamName(string teamName, Players player)
    {
        if(player == Players.First) _firstTeamName = teamName;
        else if(player == Players.Second) _secondTeamName = teamName;
    }
}
