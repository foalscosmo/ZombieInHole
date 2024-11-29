using Managers;
using UnityEngine;

namespace Hole
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private HoleScore holeScore;
        
        private void OnEnable()
        {
            holeScore.OnPlayerReachCurrentLevel += cameraFollow.UpdateTargetDistance;
        }

        private void OnDisable()
        {
            holeScore.OnPlayerReachCurrentLevel -= cameraFollow.UpdateTargetDistance;
        }
    }
}