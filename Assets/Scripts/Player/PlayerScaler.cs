using System;
using System.Collections;
using DG.Tweening;
using Ui;
using UnityEngine;

namespace Player
{
    public class PlayerScaler : MonoBehaviour
    {
        [SerializeField] private PlayerCollect playerCollect;

        [SerializeField] private PlayerScore playerScore;
        [SerializeField] private GameObject playerObj;
        private Vector3 MaxScale { get; } = new(10f, 10f, 10f);
        private readonly Vector3 _defaultScale = new(0.6f, 0.6f, 0.6f);
        private void OnEnable()
        {
            // playerCollect.OnCollectFruit += ScalePlayer;
            // playerCollect.OnDestructibleCollect += ScalePlayer;
            // playerCollect.OnBotCollect += ScalePlayer;

            playerScore.OnPlayerReachCurrentLevel += ScalePlayer;
        }

        private void OnDisable()
        {
            // playerCollect.OnCollectFruit -= ScalePlayer;
            // playerCollect.OnDestructibleCollect -= ScalePlayer;
            // playerCollect.OnBotCollect -= ScalePlayer;
            
            playerScore.OnPlayerReachCurrentLevel -= ScalePlayer;

        }
        
        private void ScalePlayer(float scaleAmount)
        {
            Vector3 current = playerObj.transform.localScale;
            var decreasedAmount = scaleAmount / 50;
            Vector3 newScale = current + new Vector3(decreasedAmount, decreasedAmount, decreasedAmount);

            newScale = Vector3.Min(newScale, MaxScale);

            playerObj.transform.DOScale(newScale, 0.5f);
        }

        public void SetDefaultScaleToPlayer()
        {
            playerObj.transform.localScale = _defaultScale;
        }
        
    }
}