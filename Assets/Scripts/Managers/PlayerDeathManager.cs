using System;
using System.Collections;
using DG.Tweening;
using InGameCamera;
using Player;
using Ui;
using UnityEngine;
using UnityEngine.UI;
using CameraFollow = Hole.CameraFollow;

namespace Managers
{
    public class PlayerDeathManager : MonoBehaviour
    {
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private Button restartButton;
        [SerializeField] private BoxCollider playerBoxColl;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private PlayerScaler playerScaler;
        [SerializeField] private PlayerScore playerScore;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private GameStageManager gameStageManager;
        [SerializeField] private PlayerMovement playerMovement;

        private void Awake()
        {
            restartButton.onClick.AddListener(HandleRestart);
            StartCoroutine(HandlePlayerUnKillable());
        }

        private void OnEnable()
        {
            playerDeath.OnPlayerDeath += HandlePlayerDeath;
            
        }

        private void OnDisable()
        {
            playerDeath.OnPlayerDeath -= HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            joystick.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            gameStageManager.IsPlayerDead = true;
            restartButton.transform.DOScale(5, 1f);
        }

        private void HandleRestart()
        {
            restartButton.transform.DOScale(0, 1f).OnComplete(()=> restartButton.gameObject.SetActive(false));
            playerDeath.gameObject.SetActive(true);
            playerScaler.SetDefaultScaleToPlayer();
            playerScore.SetPlayerStartScore();
            cameraFollow.ResetCameraDistance();
            playerScore.SetPlayerStartLevelScore();
            playerScore.SetStartKillScore();
            gameStageManager.IsPlayerDead = false;
            playerMovement.MoveSpeed = 5f;
            StartCoroutine(HandlePlayerUnKillable());
        }

        private IEnumerator HandlePlayerUnKillable()
        {
            playerRb.constraints = RigidbodyConstraints.FreezePositionY;
            playerBoxColl.enabled = false;
            yield return new WaitForSecondsRealtime(2f);
            playerBoxColl.enabled = true;
            playerRb.constraints = RigidbodyConstraints.None;
        }
    }
}