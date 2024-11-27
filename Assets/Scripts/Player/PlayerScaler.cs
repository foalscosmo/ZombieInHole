using System;
using System.Collections;
using DG.Tweening;
using Ui;
using UnityEngine;

namespace Player
{
    public class PlayerScaler : MonoBehaviour
    {
        [SerializeField] private PlayerScore playerScore;
        [SerializeField] private PlayerCollect playerCollect;
        [SerializeField] private GameObject playerObj;
        [SerializeField] private PlayerMovement playerMovement;
        private Vector3 MaxScale { get; } = new(10f, 10f, 10f);
        private readonly Vector3 defaultScale = new(0.6f, 0.6f, 0.6f);
        
        private void OnEnable()
        {
            playerScore.OnPlayerReachCurrentLevel += ScalePlayer;
        }

        private void OnDisable()
        {
            playerScore.OnPlayerReachCurrentLevel -= ScalePlayer;
        }
        
        private void ScalePlayer(float scaleAmount)
        {
            Vector3 current = playerObj.transform.localScale;
            var decreasedAmount = scaleAmount / 60;
            Vector3 newScale = current + new Vector3(decreasedAmount, decreasedAmount, decreasedAmount);
            newScale = Vector3.Min(newScale, MaxScale);
            playerObj.transform.DOScale(newScale, 0.5f);
            playerMovement.MoveSpeed += scaleAmount/15;
        }

        public void SetDefaultScaleToPlayer()
        {
            playerObj.transform.localScale = defaultScale;
        }
    }
}