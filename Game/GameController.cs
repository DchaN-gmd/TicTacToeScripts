using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameController<T> : MonoBehaviour where T : MonoBehaviour
    {
        private readonly string TapeModeKey = "TapeMode";

        [Header("Parameters")]
        [SerializeField] private float _delayBeforeStartNextRound;
        [SerializeField] private float _fillLineSpeed;

        [Header("Reference")]
        [SerializeField] private ScoreTable _scoreTable;
        [SerializeField] private PauseMenu _pauseMenu;
        [SerializeField] private PlayerMovePanel _movePanel;
        [SerializeField] private GameObject _zonesParent;
        [SerializeField] private List<TapeZone<T>> _zones;
        [SerializeField] private FinishPanel _finishPanel;

        [SerializeField] private Button _finishButton;

        [Header("Combinations")]
        [SerializeField] private List<Combination<T>> _combinations;

        private static Players _currentPlayer;

        public static Players CurrentPlayer => _currentPlayer;

        private void OnEnable()
        {
            foreach (var zone in _zones)
            {
                zone.Taped += NextMove;
            }

            _pauseMenu.CallingFinish += FinishGame;
            _finishButton?.onClick.AddListener(FinishGame);
        }

        private void OnDisable()
        {
            foreach (var zone in _zones)
            {
                zone.Taped -= NextMove;
            }

            _pauseMenu.CallingFinish -= FinishGame;
            _finishButton?.onClick.RemoveListener(FinishGame);
        }

        private void Start()
        {
            var player = Random.Range(0, 2);
            if (player == 0) _currentPlayer = Players.First;
            else _currentPlayer = Players.Second;

            StartRound();
        }

        private void StartRound()
        {
            _movePanel.ActivateCurrentPlayerImage(_currentPlayer);
            _zonesParent.SetActive(true);
        }


        private void NextMove()
        {
            if(CheckWin()) return;
            ChangePlayer();
        }

        private bool CheckWin()
        {
            foreach(var combination in _combinations)
            {
                var zones = combination.GetZones();
                if (zones.Where(x => x.CurrentActivePlayerOnZone == _currentPlayer).Count() == 3)
                {
                    combination.FillLine(_fillLineSpeed);
                    _scoreTable.UpPlayerScore(_currentPlayer);

                    NextRound();
                    return true;
                }
            }

            if (!GameModeSwitcher.IsCustomMode)
            {
                var zones = _zones.Where(x => x.CurrentActivePlayerOnZone != Players.Empty);
                if (zones.Count() == _zones.Count)
                {
                    _scoreTable.UpPlayerScore(Players.First);
                    _scoreTable.UpPlayerScore(Players.Second);

                    NextRound();
                    return true;
                }
            }

            return false;
        }

        public void FinishGame()
        {
            ShowWin();
        }

        private void ShowWin()
        {
            Players winner;

            if (_scoreTable.FirstPlayerScore > _scoreTable.SecondPlayerScore) winner = Players.First;
            else if (_scoreTable.FirstPlayerScore < _scoreTable.SecondPlayerScore) winner = Players.Second;
            else
            {
                ShowDraw();
                return;
            }
            _finishPanel.SetWinner(winner);
            _finishPanel.Show();
        }

        private void NextRound()
        {
            foreach (var currentZone in _zones)
            {
                currentZone.enabled = false;
            }
            
            Action action = ResetZones;
            action += ChangePlayer;
            Delayer.CallMethodWithDelay(_delayBeforeStartNextRound, action);
        }

        private void ResetZones()
        {
            foreach (var zone in _zones)
            {
                zone.enabled = true;
                zone.ResetZone();
            }

            foreach (var combination in _combinations)
            {
                combination.ResetLine();
            }
        }

        private void ShowDraw()
        {
            _finishPanel.Show();
            _finishPanel.SetDraw();
        }

        private void ChangePlayer()
        {
            if (_currentPlayer == Players.First) _currentPlayer = Players.Second;
            else if (_currentPlayer == Players.Second) _currentPlayer = Players.First;

            _movePanel.ActivateCurrentPlayerImage(_currentPlayer);
        }
    }
}

