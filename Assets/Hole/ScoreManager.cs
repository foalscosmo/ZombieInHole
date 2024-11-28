using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private KillZombie killZombie;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI killScore;
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

        private void Awake()
        {
            killScore.text = "Swallowed : " + currentKills;
            scoreText.text = "Zombie Score : " + currentScore;
        }

        private void SetScoreOnKill(float value)
        {
            killScore.text = $"Swallowed : {currentKills += value}";
            scoreText.text = $"Zombie Score : {currentScore += killZombie.ZombieValue}";
        }
    }
}