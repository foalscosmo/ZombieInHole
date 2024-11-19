using UnityEngine;

namespace InGameCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float followSpeed;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoomDistance;
        [SerializeField] private float maxZoomDistance;
        
        private float _currentDistance;
        private float _initialDistance; 
        private Vector3 _initialOffsetDirection;

        private void Start()
        {
            _initialOffsetDirection = offset.normalized; 
            _initialDistance = offset.magnitude;
            _currentDistance = _initialDistance; 
        }

        private void FixedUpdate()
        {
            if (playerTransform == null) return;

            Vector3 targetOffset = _initialOffsetDirection * _currentDistance;
            Vector3 targetPosition = playerTransform.position + targetOffset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        public void UpdateTargetDistance(float zoomAmount)
        {
            _currentDistance += (zoomAmount * zoomSpeed) / 7;
            _currentDistance = Mathf.Clamp(_currentDistance, minZoomDistance, maxZoomDistance); // Clamp to min and max
        }

        public void ResetCameraDistance()
        {
            _currentDistance = _initialDistance;
        }
    }
}