using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;

        private void OnEnable()
        {
            hitPointsComponent.OnHitPointsDrained += OnCharacterHitPointsDrained;
        }

        private void OnDisable()
        {
            hitPointsComponent.OnHitPointsDrained -= OnCharacterHitPointsDrained;
        }

        private void OnCharacterHitPointsDrained(GameObject _) =>
            FindObjectOfType<GameManager>().FinishGame(); // todo rework
    }
}