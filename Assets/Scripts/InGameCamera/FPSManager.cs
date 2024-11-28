using UnityEngine;

namespace InGameCamera
{
    public class FPSManager : MonoBehaviour
    {
        //[SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private float refreshRate;

        private float _timer;
        [SerializeField] private int targetFPS = 120;

        private void Start()
        {
            Application.targetFrameRate = targetFPS;
            QualitySettings.vSyncCount = 0;
        }
        // private void Update()
        // {
        //     _timer += Time.deltaTime;
        //     if (!(_timer >= refreshRate)) return;
        //     var fps = Mathf.RoundToInt(1f / Time.deltaTime);
        //     fpsText.text = $"FPS: {fps}";
        //     _timer = 0f;
        //
        // }
    }
}