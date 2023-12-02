using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IGameEvent.IEndGameListener
    {
        public void OnGameLoosed()
        {
            FinishGame();
        }

        private static void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}