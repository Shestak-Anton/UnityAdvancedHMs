using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour,
        IGameEvent.IEndGameListener,
        IGameEvent.IStartGameListener
    {
        void IGameEvent.IEndGameListener.OnEndGame()
        {
            
        }

        public void OnGameStarted()
        {
            
        }
    }
}