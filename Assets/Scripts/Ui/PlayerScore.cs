using System;
using System.Globalization;
using Managers;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI levelScoreText;
        [SerializeField] private TextMeshProUGUI levelIndexText;
        [SerializeField] private TextMeshProUGUI currentKillText;
        [SerializeField] private int currentLevelIndex;
        [SerializeField] private float currentLevelScore;
        [SerializeField] private float currentLevelMaxScore;
        [SerializeField] private float currentScore;
        [SerializeField] private int currentKillScore;
        [SerializeField] private RankScore rankScore;
        public event Action<float> OnPlayerReachCurrentLevel;
        
        private TextMeshProUGUI ScoreText
        {
            get => scoreText;
            set => scoreText = value;
        }

        public float CurrentScore
        {
            get => currentScore;
            set => currentScore = value;
        }
        
        
        private void Start()
        {
            SetPlayerStartScore();
            SetLevelIndexText();
            currentKillText.text = $"Kills : {currentKillScore}";
            levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;
        }

        private void Update()
        {
            SetLevelMaxScore();
        }

        public void SetPlayerScore(float amount)
        {
            currentScore += amount;
            rankScore.score = currentScore;
            ScoreText.text = "SCORE : " + currentScore.ToString(CultureInfo.InvariantCulture);
        }

        public void SetPlayerStartScore()
        {
            currentScore = 0;
            ScoreText.text = "SCORE : " + currentScore.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetPlayerStartLevelScore()
        {
            currentLevelScore = 0;
            currentLevelIndex = 0;
            currentKillScore = 0;
            currentLevelMaxScore = 30;
            levelIndexText.text = "Level " + (currentLevelIndex+1);
            levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;
        }

        public void SetPlayerLevelScore(float amount)
        {
            currentLevelScore ++;
            levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;
        }
        
        public void SetStartKillScore()
        {
            currentKillScore++;
            currentKillText.text = $"Kills : {currentKillScore}";
        }
        public void SetKillScore(float amount)
        {
            currentKillScore++;
            currentKillText.text = $"Kills : {currentKillScore}";
        }

        private void SetLevelIndexText()
        {
            levelIndexText.text = "Level " + (currentLevelIndex+1);
        }

        private void SetLevelMaxScore()
        {
            if (currentLevelScore >= currentLevelMaxScore)
            {
                currentLevelIndex++;
                currentLevelScore = 0;
                OnPlayerReachCurrentLevel?.Invoke(40f);
                switch (currentLevelIndex)
                {
                    case 1:
                        currentLevelMaxScore = 70;
                        break;
                    case 2:
                        currentLevelMaxScore = 140;
                        break;
                    case 3:
                        currentLevelMaxScore = 500;
                        break;
                    case 4:
                        currentLevelMaxScore = 1200;
                        break;
                }

                SetLevelIndexText();
                levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;

            }
        }
    }
}