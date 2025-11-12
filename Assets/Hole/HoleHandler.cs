using DG.Tweening;
using Managers;
using Player;
using UnityEngine;

namespace Hole
{
    public class HoleHandler : MonoBehaviour
    {
        [SerializeField] private int normalSphereLayer;
        [SerializeField] private int fallingSphereLayer;
        [SerializeField] private HoleScore holeScore;
        [SerializeField] private MovementWithTransform movementWithTransform;
        private void OnEnable()
        {
            holeScore.OnPlayerReachCurrentLevel += ScalePlayer;
        }

        private void OnDisable()
        {
            holeScore.OnPlayerReachCurrentLevel -= ScalePlayer;
        }
    
        private void ScalePlayer(float scaleAmount)
        {
            Vector3 current = transform.transform.localScale;
            var decreasedAmount = scaleAmount / 20f;
            Vector3 newScale = current + new Vector3(decreasedAmount,0, decreasedAmount);
            transform.transform.DOScale(newScale, 0.5f);
            movementWithTransform.MoveSpeed += scaleAmount / 8f;
        }

        public void ResetPlayer()
        {
            transform.transform.DOScale(1, 0.5f);
            transform.position = new  Vector3(0, 1.075f, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == normalSphereLayer)
            {
                other.gameObject.layer = fallingSphereLayer;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == fallingSphereLayer)
            {
                other.gameObject.layer = normalSphereLayer;
            }
        }
    }
}