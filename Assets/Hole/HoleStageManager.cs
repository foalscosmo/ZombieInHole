using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hole
{
    public class HoleStageManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startTimeText;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private int gameStageTimes;
        [SerializeField] private int currentStage = 0;
        [SerializeField] private HoleScore holeScore;
        [SerializeField] private List<SpherePoolManager> spherePoolManager = new();
        private float _currentCountdownTime;

        private void OnEnable()
        {
            holeScore.OnPlayerReachCurrentLevel += SetStageSize;
        }

        private void OnDisable()
        {
            holeScore.OnPlayerReachCurrentLevel -= SetStageSize;
        }
        

        private void Start()
        {
            _currentCountdownTime = gameStageTimes;
            losePanel.SetActive(false);
        }
        
        private void Update()
        {
            _currentCountdownTime -= Time.deltaTime; 
            UpdateTimeDisplay(_currentCountdownTime);
            
            if (_currentCountdownTime <= 0f)
            {
                losePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void UpdateTimeDisplay(float remainingTime)
        {
            remainingTime = Mathf.Max(remainingTime, 0);
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            startTimeText.text = $"Time: {minutes:00}:{seconds:00}";

            startTimeText.color = remainingTime <= 5f ? Color.red : Color.blue;
        }
        
        public void ResetTimer()
        {
            _currentCountdownTime = gameStageTimes;
        }

        private void SetStageSize(float _)
        {
            foreach (var pool in spherePoolManager)
            {
                pool.BotMaxSize += 1f;
                pool.BotsMinSize += 1f;
            }
            
            foreach (var pool in spherePoolManager)
            {
                pool.BotMaxSize += 1f;
                pool.BotsMinSize += 1f;
            }
        }
    }
}