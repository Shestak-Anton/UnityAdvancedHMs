using UnityEngine;

namespace ShootEmUp
{
    public class EndGameObserver : MonoBehaviour
    {

        [SerializeField] private HitPointsComponent hitPointsComponent;
        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            hitPointsComponent.OnHpEmpty += FinishGame;
        }

        private void OnDisable()
        {
            hitPointsComponent.OnHpEmpty -= FinishGame;
        }

        private void FinishGame(GameObject gameObject)
        {
            gameManager.FinishGame();
        }
    }
}