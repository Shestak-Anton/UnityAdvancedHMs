using GameLoop;
using Ui;
using UnityEngine;

namespace ShootEmUp
{
    public class StartGameObserver : MonoBehaviour
    {
        [SerializeField] private PauseScreenComponent _pauseScreen;
        [SerializeField] private GameStateService _gameStateService;

        private void OnEnable()
        {
            _pauseScreen.OnStartGameListener += StartGame;
        }

        private void OnDisable()
        {
            _pauseScreen.OnStartGameListener -= StartGame;
        }

        private void StartGame()
        {
            _gameStateService.ApplyState(GameState.GamePlay);
        }
    }
}