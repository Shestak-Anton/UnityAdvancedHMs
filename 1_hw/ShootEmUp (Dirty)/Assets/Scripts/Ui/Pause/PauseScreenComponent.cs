using System;
using GameLoop;
using UnityEngine;

namespace Ui
{
    public class PauseScreenComponent : MonoBehaviour,
        IGameEvent.IPauseGameListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IEndGameListener
    {
        public event Action OnStartGameListener;

        [SerializeField] private StartGameTimerComponent _startGameTimer;
        [SerializeField] private GameObject _startButton;

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            EnableContent(true);
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            EnableContent(false);
        }

        public void OnStartButtonClicked()
        {
            _startButton.SetActive(false);
            _startGameTimer.StartTimer(OnStartGameListener);
        }


        void IGameEvent.IEndGameListener.OnEndGame()
        {
            
            EnableContent(true);
        }

        private void EnableContent(bool show)
        {
            gameObject.SetActive(show);
            _startButton.SetActive(show);
        }
    }
}