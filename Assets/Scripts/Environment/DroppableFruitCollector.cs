using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class DroppableFruitCollector : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private LayerMask botLayerMask;
        [SerializeField] private float scaleDuration = 0.3f; 
        [SerializeField] private float moveDuration = 0.3f;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & playerLayerMask) != 0)
            {
                _boxCollider.enabled = false;
                CollectFruitOnPlayerTrigger(other.transform);
                
            }else if (((1 << other.gameObject.layer) & botLayerMask) != 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void CollectFruitOnPlayerTrigger(Transform playerTransform)
        {
            Vector3 targetPosition = playerTransform.position; // Target is player's position.

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetPosition, moveDuration)) // Move towards the player
                .Join(transform.DOScale(Vector3.zero, scaleDuration))   // Scale down during movement
                .OnComplete(() =>
                {
                    gameObject.SetActive(false); // Hide the fruit once collected
                });
        }
    }
}