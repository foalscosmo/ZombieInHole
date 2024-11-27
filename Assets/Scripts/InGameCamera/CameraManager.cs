using Ui;
using UnityEngine;

namespace InGameCamera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private PlayerScore playerScore;
        
        private void OnEnable()
        {
            playerScore.OnPlayerReachCurrentLevel += cameraFollow.UpdateTargetDistance;
        }

        private void OnDisable()
        {
            playerScore.OnPlayerReachCurrentLevel -= cameraFollow.UpdateTargetDistance;
        }
    }
}