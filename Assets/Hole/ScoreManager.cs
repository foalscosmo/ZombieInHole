using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private KillZombie killZombie;
        [SerializeField] private HoleScore holeScore;
        [SerializeField] private HoleStageManager holeStageManager;
        [SerializeField] private int currentScore;
        [SerializeField] private float currentKills;
        
        private void OnEnable()
        {
            killZombie.OnZombieKill += SetScoreOnKill;
        }

        private void OnDisable()
        {
            killZombie.OnZombieKill -= SetScoreOnKill;
        }
        
        private void SetScoreOnKill(int value)
        {
            holeScore.KillsText.text = $"Swallowed : {currentKills += 1}";
            holeScore.ScoreText.text = $"Zombie Score : {currentScore += killZombie.ZombieValue}";
            holeScore.SetPlayerScore(value);
            holeScore.SetPlayerLevelScore(value);
            holeStageManager.ResetTimer();
        }

        public void ResetScore()
        {
            currentScore = 0;
            holeScore.KillsText.text = $"Swallowed : {currentKills}";
            holeScore.ScoreText.text = $"Zombie Score : {currentScore}";
            holeScore.SetPlayerScore(0);
            holeScore.RestartPlayerLevelScore(0);
        }
    }
}