using UnityEngine;

namespace InGameCamera
{
    public class FaceCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public Camera MainCamera
        {
            get => mainCamera;
            set => mainCamera = value;
        }
        
        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }
}
