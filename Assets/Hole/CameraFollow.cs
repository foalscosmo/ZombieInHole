using System.Collections;
using AI;
using UnityEngine;

namespace Hole
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float followSpeed;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float maxZoomDistance;
        [SerializeField] private float cameraAdjustValue;
        [SerializeField] private DebugPanel debugPanel;
        [SerializeField] private float cameraZoomOutValue;

        public float CameraZoomOutValue
        {
            get => cameraZoomOutValue;
            set => cameraZoomOutValue = value;
        }
        
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
            Vector3 targetOffset = _initialOffsetDirection * _currentDistance;
            Vector3 targetPosition = playerTransform.position + targetOffset;

            transform.position =
                Vector3.Lerp(new Vector3(transform.position.x + cameraAdjustValue, transform.position.y, transform.position.z),
                    targetPosition, followSpeed * Time.deltaTime);
        }
        
        public void UpdateTargetDistance(float zoomAmount)
        {
            StartCoroutine(FollowCameraZoomTimer(zoomAmount));
        }

        private IEnumerator FollowCameraZoomTimer(float zoomAmount)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            cameraAdjustValue += zoomAmount/10f;
            _currentDistance += (zoomAmount * zoomSpeed) / cameraZoomOutValue;
            debugPanel.ZoomOutValueSlider.value = cameraZoomOutValue;
            _currentDistance = Mathf.Clamp(_currentDistance, 5, maxZoomDistance); 
        }

        public void ResetCameraDistance()
        {
            _currentDistance = _initialDistance;
        }
    }
}