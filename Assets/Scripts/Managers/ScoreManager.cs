using System;
using System.Collections.Generic;
using Player;
using Ui;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private PlayerScore playerScore;
        [SerializeField] private PlayerCollect playerCollect;
        [SerializeField] private RankScore playerRankScore;
        
        private void OnEnable()
        {
            //current Score
            playerCollect.OnCollectFruit += playerScore.SetPlayerScore;
            playerCollect.OnDestructibleCollect += playerScore.SetPlayerScore;
            playerCollect.OnBotCollect += playerScore.SetPlayerScore;
            playerCollect.OnBotCollect += playerScore.SetKillScore;
            
            //level index
            playerCollect.OnCollectFruit += playerScore.SetPlayerLevelScore;
            playerCollect.OnDestructibleCollect += playerScore.SetPlayerLevelScore;
            playerCollect.OnBotCollect += playerScore.SetPlayerLevelScore;
            
        }

        private void OnDisable()
        {
            playerCollect.OnCollectFruit -= playerScore.SetPlayerScore;
            playerCollect.OnDestructibleCollect -= playerScore.SetPlayerScore;
            playerCollect.OnBotCollect -= playerScore.SetPlayerScore;
            playerCollect.OnBotCollect -= playerScore.SetKillScore;

            
            
            playerCollect.OnCollectFruit -= playerScore.SetPlayerLevelScore;
            playerCollect.OnDestructibleCollect -= playerScore.SetPlayerLevelScore;
            playerCollect.OnBotCollect -= playerScore.SetPlayerLevelScore;
        }

        private void Awake()
        {
            playerRankScore.gameObject.name = "FoalsCosmo";
        }
    }
}