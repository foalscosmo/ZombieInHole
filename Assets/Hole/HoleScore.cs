using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class HoleScore : MonoBehaviour
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
        public event Action<float> OnPlayerReachCurrentLevel;
        
        public TextMeshProUGUI ScoreText
        {
            get => scoreText;
            set => scoreText = value;
        }

        public TextMeshProUGUI KillsText
        {
            get => currentKillText;
            set => currentKillText = value;
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
            SetPlayerStartLevelScore();
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
            ScoreText.text = "ZOMBIE SCORE : " + currentScore.ToString(CultureInfo.InvariantCulture);
        }

        private void SetPlayerStartScore()
        {
            currentScore = 0;
            ScoreText.text = "ZOMBIE SCORE : " + currentScore.ToString(CultureInfo.InvariantCulture);
        }
        
        public void SetPlayerStartLevelScore()
        {
            currentLevelScore = 0;
            currentLevelIndex = 0;
            currentKillScore = 0;
            currentLevelMaxScore = 15;
            levelIndexText.text = "Level " + (currentLevelIndex+1);
            levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;
        }

        public void SetPlayerLevelScore(float amount)
        {
            currentLevelScore ++;
            levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;
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
                OnPlayerReachCurrentLevel?.Invoke(10f);
                currentLevelMaxScore += 10;
                SetLevelIndexText();
                levelScoreText.text = currentLevelScore + " / " + currentLevelMaxScore;

            }
        }
    }
}