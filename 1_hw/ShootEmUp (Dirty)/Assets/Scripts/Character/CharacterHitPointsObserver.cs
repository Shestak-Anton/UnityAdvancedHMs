using GameLoop;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterHitPointsObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private GameStateService _gameStateService;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _hitPointsComponent.OnHpEmpty += FinishGame;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _hitPointsComponent.OnHpEmpty -= FinishGame;
        }

        private void FinishGame(GameObject _)
        {
            _gameStateService.ApplyState(GameState.EndGame);
        }
    }
}