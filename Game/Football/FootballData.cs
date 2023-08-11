using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Football Data", menuName = "Football Data", order = 52)]
public class FootballData : ScriptableObject
{
    [SerializeField] private Sprite _teamLogo;
    [SerializeField] private Sprite _teamPlayer;
    [SerializeField] private string _teamName;
    [SerializeField] private string _fullTeamName;

    public Sprite TeamLogo => _teamLogo;
    public Sprite TeamPlayer => _teamPlayer;
    public string TeamName => _teamName;
    public string FullTeamName => _fullTeamName;
}
