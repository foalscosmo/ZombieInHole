using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class HoleStageManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startTimeText;
        [SerializeField] private GameObject adCanvas;
        [SerializeField] private int gameStageTimes;
        [SerializeField] private int currentStage = 0;
        [SerializeField] private bool isTimeForAd = false;
        private float _currentCountdownTime;
        [SerializeField] private HoleScore holeScore;
        [SerializeField] private List<SpherePoolManager> spherePoolManager = new();

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
            adCanvas.SetActive(false);
        }
        
        private void Update()
        {
            if (isTimeForAd) return;

            _currentCountdownTime -= Time.deltaTime; 
            UpdateTimeDisplay(_currentCountdownTime);
            
            if (_currentCountdownTime <= 0f)
            {
                isTimeForAd = true;
                adCanvas.SetActive(true); 
                ShowAd();
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

        private void ShowAd()
        {
            Debug.Log("Showing Add...");
        }

        public void OnAdFinished()
        {
            isTimeForAd = false;
            adCanvas.SetActive(false);
            Time.timeScale = 1;
            currentStage++; 
            _currentCountdownTime = gameStageTimes;
        }

        private void SetStageSize(float index)
        {
            switch (currentStage)
            {
                case 3:
                    foreach (var pool in spherePoolManager)
                    {
                        pool.BotMaxSize += 1f;
                        pool.BotsMinSize += 1f;
                    }
                    break;
                case 6:
                    foreach (var pool in spherePoolManager)
                    {
                        pool.BotMaxSize += 1f;
                        pool.BotsMinSize += 1f;
                    }
                    break;
            }
            
        }
    }
}