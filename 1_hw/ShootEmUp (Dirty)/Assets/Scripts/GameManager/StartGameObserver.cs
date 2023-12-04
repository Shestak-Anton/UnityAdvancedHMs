using GameLoop;
using LifeCycle;
using Ui;
using UnityEngine;

namespace ShootEmUp
{
    public class StartGameObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private PauseScreenComponent _pauseScreen;
        [SerializeField] private GameStateService _gameStateService;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _pauseScreen.OnStartGameListener += StartGame;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _pauseScreen.OnStartGameListener -= StartGame;
        }

        private void StartGame()
        {
            _gameStateService.ApplyState(GameState.GamePlay);
        }
    }
}