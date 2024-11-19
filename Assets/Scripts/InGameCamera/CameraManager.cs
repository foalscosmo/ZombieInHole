using Player;
using Ui;
using UnityEngine;

namespace InGameCamera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private PlayerCollect playerCollect;
        [SerializeField] private CameraFollow cameraFollow;

        [SerializeField] private PlayerScore playerScore;
        
        private void OnEnable()
        {
            // playerCollect.OnCollectFruit += cameraFollow.UpdateTargetDistance;
            // playerCollect.OnDestructibleCollect += cameraFollow.UpdateTargetDistance;
            
            playerCollect.OnBotCollect += cameraFollow.UpdateTargetDistance;

            playerScore.OnPlayerReachCurrentLevel += cameraFollow.UpdateTargetDistance;
        }

        private void OnDisable()
        {
            //playerCollect.OnCollectFruit -= cameraFollow.UpdateTargetDistance;
            //playerCollect.OnDestructibleCollect -= cameraFollow.UpdateTargetDistance;
            //playerCollect.OnBotCollect -= cameraFollow.UpdateTargetDistance;
            
            playerScore.OnPlayerReachCurrentLevel -= cameraFollow.UpdateTargetDistance;
        }
    }
}