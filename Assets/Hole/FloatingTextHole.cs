using UnityEngine;

namespace Hole
{
    public class FloatingTextHole : MonoBehaviour
    {
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (mainCamera != null)
            {
                transform.LookAt(mainCamera.transform);
                transform.Rotate(0, 180, 0);
            }
        }
    }
}