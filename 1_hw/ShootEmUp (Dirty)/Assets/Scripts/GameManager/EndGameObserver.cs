using UnityEngine;

namespace ShootEmUp
{
    public sealed class EndGameObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;

        private void OnEnable()
        {
            hitPointsComponent.OnHpEmptyListener += FinishGame;
        }

        private void OnDisable()
        {
            hitPointsComponent.OnHpEmptyListener -= FinishGame;
        }

        private static void FinishGame(GameObject gameObject)
        {
            GameManager.FinishGame();
        }
    }
}