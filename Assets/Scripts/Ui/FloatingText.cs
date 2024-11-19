using UnityEngine;

namespace Ui
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float destroyTime;
        [SerializeField] private Vector3 spawnY;
        private readonly Vector3 _randomizeIntensity = new(0.5f, 0, 0);

        private void Start()
        {
            Destroy(this.gameObject,destroyTime);
            var localPosition = transform.localPosition;
            localPosition += spawnY;
            localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x),
                _randomizeIntensity.y,
                Random.Range(-_randomizeIntensity.z, _randomizeIntensity.z));
            transform.localPosition = localPosition;
        }
    }
}