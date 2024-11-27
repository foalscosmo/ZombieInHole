using DG.Tweening;
using Player;
using UnityEngine;

namespace Hole
{
    public class HoleHandler : MonoBehaviour
    {
        [SerializeField] private int normalSphereLayer;
        [SerializeField] private int fallingSphereLayer;
        [SerializeField] private KillZombie killZombie;
        [SerializeField] private MovementWithTransform movementWithTransform;
        private void OnEnable()
        {
            killZombie.OnZombieKill += ScalePlayer;
        }

        private void OnDisable()
        {
            killZombie.OnZombieKill -= ScalePlayer;
        }
    
        private void ScalePlayer(float scaleAmount)
        {
            Vector3 current = transform.transform.localScale;
            var decreasedAmount = scaleAmount / 20f;
            Vector3 newScale = current + new Vector3(decreasedAmount,0, decreasedAmount);
            transform.transform.DOScale(newScale, 0.5f);
            movementWithTransform.MoveSpeed += scaleAmount / 20f;
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