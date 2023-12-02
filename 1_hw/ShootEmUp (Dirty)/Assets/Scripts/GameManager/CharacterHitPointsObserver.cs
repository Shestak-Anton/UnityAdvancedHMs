using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterHitPointsObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private GameStateService _gameStateService;

        private void OnEnable()
        {
            _hitPointsComponent.OnHpEmptyListener += FinishGame;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnHpEmptyListener -= FinishGame;
        }

        private void FinishGame(GameObject _)
        {
            _gameStateService.ApplyState(GameState.EndGame);
        }
    }
}