using System;
using GameLoop;
using LifeCycle;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class PauseScreenComponent : MonoBehaviour,
        IGameEvent.IPauseGameListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IEndGameListener,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        public event Action OnStartGame;

        [SerializeField] private StartGameTimerComponent _startGameTimer;
        [SerializeField] private Button _startButton;

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            EnableContent(true);
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            EnableContent(false);
        }

        private void OnStartButtonClicked()
        {
            _startButton.gameObject.SetActive(false);
            _startGameTimer.StartTimer(OnStartGame);
        }


        void IGameEvent.IEndGameListener.OnEndGame()
        {
            EnableContent(true);
        }

        private void EnableContent(bool show)
        {
            gameObject.SetActive(show);
            _startButton.gameObject.SetActive(show);
        }

        public void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        public void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
    }
}