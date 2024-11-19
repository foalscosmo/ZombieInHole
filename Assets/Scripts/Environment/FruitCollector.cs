using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class FruitCollector : MonoBehaviour
    {
        public delegate void FruitCollectedHandler(GameObject fruit);
        public static event FruitCollectedHandler OnFruitCollectedPlayer;
        public static event FruitCollectedHandler OnFruitCollectedBot;

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
                OnFruitCollectedPlayer?.Invoke(gameObject);
                CollectFruitOnPlayerTrigger(other.transform);
                
            }else if (((1 << other.gameObject.layer) & botLayerMask) != 0)
            {
                OnFruitCollectedBot?.Invoke(gameObject);
                gameObject.SetActive(false);
            }
        }

        private void CollectFruitOnPlayerTrigger(Transform playerTransform)
        {
            Vector3 targetPosition = playerTransform.position;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetPosition, moveDuration)) 
                .Join(transform.DOScale(Vector3.zero, scaleDuration))  
                .OnComplete(() =>
                {
                    _boxCollider.enabled = true;
                    gameObject.SetActive(false); 
                });
        }
    }
}
