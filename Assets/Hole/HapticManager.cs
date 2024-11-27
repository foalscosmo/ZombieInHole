using UnityEngine;
using CandyCoded.HapticFeedback;

namespace Hole
{
    public class HapticManager : MonoBehaviour
    {
        [SerializeField] private KillZombie zombieCollect;
        [SerializeField] private float hapticCooldown; 
        private float lastHapticTime;
        private void OnEnable()
        {
            zombieCollect.OnZombieKill += HandleHapticsWhenCollect;
        }

        private void OnDisable()
        {
            zombieCollect.OnZombieKill -= HandleHapticsWhenCollect;
        }
        
        private void HandleHapticsWhenCollect(float index)
        {
            if (Time.time - lastHapticTime >= hapticCooldown)
            {
                lastHapticTime = Time.time;

                switch (index)
                {
                    case 1f:
                        HapticFeedback.MediumFeedback();
                        break;
                }
            }
        }
    }
}