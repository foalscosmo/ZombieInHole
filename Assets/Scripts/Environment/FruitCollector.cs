using System.Collections;
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
        [SerializeField] private float moveDuration;
        private BoxCollider boxCollider;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & playerLayerMask) != 0)
            {
                boxCollider.enabled = false;
                OnFruitCollectedPlayer?.Invoke(gameObject);
                StartCoroutine(CollectFruitOnPlayerTrigger(other.transform));
            }
            else if (((1 << other.gameObject.layer) & botLayerMask) != 0)
            {
                OnFruitCollectedBot?.Invoke(gameObject);
                gameObject.SetActive(false);
            }
        }

        private IEnumerator CollectFruitOnPlayerTrigger(Transform playerTransform)
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = playerTransform.position;
            Vector3 startScale = transform.localScale;
            Vector3 targetScale = Vector3.zero;

            float elapsedTime = 0f;

            // Move and scale over time
            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;

                float t = elapsedTime / moveDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t); // Smooth movement
                transform.localScale = Vector3.Lerp(startScale, targetScale, t); // Smooth scaling

                yield return null;
            }

            // Ensure the final state is reached
            transform.position = targetPosition;
            transform.localScale = targetScale;

            gameObject.SetActive(false);
            boxCollider.enabled = true;
        }
    }
}
