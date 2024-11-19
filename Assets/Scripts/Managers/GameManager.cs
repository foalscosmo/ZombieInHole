using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startTimeText;
        [SerializeField] private GameObject adCanvas;
        [SerializeField] private int gameStageTimes;
        [SerializeField] private int currentStage = 0;
        [SerializeField] private bool isTimeForAd = false;
        
        private float _currentCountdownTime; 

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
            startTimeText.text = $"Stage {currentStage+1} Time : {minutes:00}:{seconds:00}";
        }

        private void ShowAd()
        {
            Debug.Log("Showing ad...");
        }

        public void OnAdFinished()
        {
            isTimeForAd = false;
            adCanvas.SetActive(false);
            Time.timeScale = 1;
            currentStage++; 
            _currentCountdownTime = gameStageTimes;
        }
    }
}