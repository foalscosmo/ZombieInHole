using UnityEngine;

namespace Hole
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private KillZombie playerScore;
        
        private void OnEnable()
        {
            playerScore.OnZombieKill += cameraFollow.UpdateTargetDistance;
        }

        private void OnDisable()
        {
            playerScore.OnZombieKill -= cameraFollow.UpdateTargetDistance;
        }
    }
}