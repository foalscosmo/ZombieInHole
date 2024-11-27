using UnityEngine;

namespace Hole
{
    public class FloatingTextHole : MonoBehaviour
    {
        // [SerializeField] private float destroyTime;
        // [SerializeField] private Vector3 spawnY;
        // private readonly Vector3 _randomizeIntensity = new(0.5f, 0, 0);
        //
        // private void Start()
        // {
        //     Destroy(this.gameObject,destroyTime);
        //     var localPosition = transform.localPosition;
        //     localPosition += spawnY;
        //     localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x),
        //         _randomizeIntensity.y,
        //         Random.Range(-_randomizeIntensity.z, _randomizeIntensity.z));
        //     transform.localPosition = localPosition;
        // }
        
        [SerializeField] private float destroyTime = 2f; // Default value
        [SerializeField] private Vector3 spawnY = new Vector3(0, 1, 0);
        private readonly Vector3 _randomizeIntensity = new Vector3(0.5f, 0, 0);

        private Camera mainCamera;

        private void Start()
        {
            // Destroy the text after the specified time
            Destroy(gameObject, destroyTime);

            // Find the main camera (assumes the active camera is tagged "MainCamera")
            mainCamera = Camera.main;

            // Adjust the position for spawning and randomize slightly
            var localPosition = transform.localPosition;
            localPosition += spawnY;
            localPosition += new Vector3(
                Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x),
                _randomizeIntensity.y,
                Random.Range(-_randomizeIntensity.z, _randomizeIntensity.z)
            );
            transform.localPosition = localPosition;
        }

        private void Update()
        {
            if (mainCamera != null)
            {
                // Make the prefab's text face the camera
                transform.LookAt(mainCamera.transform);
                // Adjust for correct orientation (reversing the rotation)
                transform.Rotate(0, 180, 0);
            }
        }
    }
}