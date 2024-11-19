using System.Globalization;
using Managers;
using TMPro;
using UnityEngine;

namespace AI
{
    public class BotScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private float currentScore;
        [SerializeField] private BotCollect botCollect;
        [SerializeField] private RankScore rankScore;

        public float CurrentScore
        {
            get => currentScore;
            set => currentScore = value;
        }

        private void OnEnable()
        {
            botCollect.OnCollectFruit += SetBotScore;
            botCollect.OnDestructibleCollect += SetBotScore;
            botCollect.OnPlayerCollect += SetBotScore;
        }

        private void OnDisable()
        {
            botCollect.OnCollectFruit -= SetBotScore;
            botCollect.OnDestructibleCollect -= SetBotScore;
            botCollect.OnPlayerCollect -= SetBotScore;
        }

        public void InitializeScore(float initialScore)
        {
            currentScore = Mathf.RoundToInt(initialScore);
            rankScore.score = currentScore;
            UpdateScoreText();
        }

        private void SetBotScore(float amount)
        {
            currentScore += amount;
            rankScore.score = currentScore;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = currentScore.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}