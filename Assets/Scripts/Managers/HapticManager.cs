using CandyCoded.HapticFeedback;
using Player;
using UnityEngine;

namespace Managers
{
    public class HapticManager : MonoBehaviour
    {
        [SerializeField] private PlayerCollect playerCollect;
        private void OnEnable()
        {
            playerCollect.OnCollectFruit += HandleHapticsWhenCollect;
            playerCollect.OnDestructibleCollect += HandleHapticsWhenCollect;
        }

        private void OnDisable()
        {
            playerCollect.OnCollectFruit -= HandleHapticsWhenCollect;
            playerCollect.OnDestructibleCollect -= HandleHapticsWhenCollect;
        }

        private void HandleHapticsWhenCollect(float index)
        {
            switch (index)
            {
                case 1f:
                    HapticFeedback.MediumFeedback();
                    break;
                case 2f:
                    HapticFeedback.MediumFeedback();
                    break;
                case 4f:
                    HapticFeedback.MediumFeedback();
                    break;
                case 8f:
                    HapticFeedback.MediumFeedback();
                    break;
            }
        }
    }
}