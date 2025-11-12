using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hole
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private HoleHandler holeHandler;
        [SerializeField] private HoleStageManager holeStageManager;

        private void Awake()
        {
            restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            Time.timeScale = 1;
            holeStageManager.ResetTimer();
            scoreManager.ResetScore();
            cameraFollow.ResetCameraDistance();
            holeHandler.ResetPlayer();
            gameOverPanel.SetActive(false);
        }
    }
}