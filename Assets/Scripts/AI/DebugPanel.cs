using System.Globalization;
using DG.Tweening;
using Hole;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField] private MovementWithTransform playerMovement;
        [SerializeField] private Slider moveSpeedSlider;
        [SerializeField] private TextMeshProUGUI moveSpeedValueText;

        
        [SerializeField] private CameraFollow cameraFollow;
        // [SerializeField] private Slider cameraDistanceSlider;
        
        [SerializeField] private Slider cameraZoomOutValueSlider;
        [SerializeField] private TextMeshProUGUI zoomOutValueText;

        
        
        public Slider ZoomOutValueSlider
        {
            get => cameraZoomOutValueSlider;
            set => cameraZoomOutValueSlider = value;
        }
        
        [SerializeField] private GameObject debugPanel;
        [SerializeField] private Button debugOpenButton;
        [SerializeField] private Button debugCloseButton;

        private void Awake()
        {
            debugOpenButton.onClick.AddListener(OpenDebugPanel);
            debugCloseButton.onClick.AddListener(CloseDebugPanel);
            debugPanel.transform.localScale = Vector3.zero;
            moveSpeedSlider.value = playerMovement.MoveSpeed;
            cameraZoomOutValueSlider.value = cameraFollow.CameraZoomOutValue;
            moveSpeedValueText.text = moveSpeedSlider.value.ToString(CultureInfo.InvariantCulture);
            zoomOutValueText.text = cameraZoomOutValueSlider.value.ToString(CultureInfo.InvariantCulture);
        }

        private void OpenDebugPanel()
        {
            debugPanel.SetActive(true);
            debugPanel.transform.DOScale(1, 0.3f).OnComplete(() =>
            {
                debugOpenButton.gameObject.SetActive(false);
                Time.timeScale = 0;
            });
        }

        private void CloseDebugPanel()
        {
            debugPanel.SetActive(false);
            debugOpenButton.gameObject.SetActive(true);
            Time.timeScale = 1;
        }

        public void SetMoveSpeedWithSlider()
        {
            playerMovement.MoveSpeed = moveSpeedSlider.value;
            moveSpeedValueText.text = moveSpeedSlider.value.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetCameraZoomOutValue()
        {
            cameraFollow.CameraZoomOutValue = cameraZoomOutValueSlider.value;
            zoomOutValueText.text = cameraZoomOutValueSlider.value.ToString(CultureInfo.InvariantCulture);
        }
        
    }
}