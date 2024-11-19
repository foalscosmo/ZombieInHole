using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class DestructibleObjects : MonoBehaviour
    {
            [SerializeField] private LayerMask playerLayerMask;
            [SerializeField] private LayerMask botLayerMask;
            private Vector3 _originalScale;
            private BoxCollider _boxCollider;

            private void Awake()
            {
                _boxCollider = GetComponent<BoxCollider>();
            }

            private void Start()
            {
                _originalScale = transform.localScale;
            }

            private void OnTriggerEnter(Collider other)
            {
                if (((1 << other.gameObject.layer) & playerLayerMask | botLayerMask) != 0)
                {
                    _boxCollider.enabled = false;
                    transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                    {
                        StartCoroutine(ResetScaleAfterDelay(20f));
                    });
                }
            }
            
            private IEnumerator ResetScaleAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);
                _boxCollider.enabled = true;
                transform.DOScale(_originalScale, 0.5f);
            }
    }
}