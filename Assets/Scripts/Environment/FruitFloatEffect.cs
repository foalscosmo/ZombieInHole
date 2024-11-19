using DG.Tweening;
using UnityEngine;

namespace Environment
{
    public class FruitFloatEffect : MonoBehaviour
    {
        [SerializeField] private float floatDistance = 0.5f; 
        [SerializeField] private float floatDuration = 2f; 
        [SerializeField] private float rotationSpeed; 
        [SerializeField] private Ease floatEase = Ease.InOutSine;

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;  

            StartFloating();
            StartRotating();
        }

        private void StartFloating()
        {
            transform.DOMoveY(_initialPosition.y + floatDistance, floatDuration)
                .SetEase(floatEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void StartRotating()
        {
            transform.DORotate(new Vector3(0f, rotationSpeed, 0f), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental); 
        }
    }
}