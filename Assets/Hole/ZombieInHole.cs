using System;
using AI;
using UnityEngine;

namespace Hole
{
    public class ZombieInHole : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private BotMovement botMovement;
        private float startSpeed;

        public event Action<GameObject> OnZombieKilled;

        private int playerNoseLayer;
        private int playerLayer;

        private void Awake()
        {
            playerNoseLayer = LayerMask.NameToLayer("PlayerNose");
            playerLayer = LayerMask.NameToLayer("Player");
        }

        private void Start()
        {
            if (botMovement != null) startSpeed = botMovement.MoveSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == playerNoseLayer)
            {
                StopBotMovement();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == playerLayer)
            {
                EnableGravity();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == playerNoseLayer)
            {
                ResumeBotMovement();
                OnZombieKilled?.Invoke(gameObject);
            }

            if (other.gameObject.layer == playerLayer)
            {
                EnableGravity();
            }
        }

        private void StopBotMovement()
        {
            if (botMovement != null)
            {
                botMovement.MoveSpeed = 0;
            }
        }

        private void ResumeBotMovement()
        {
            if (botMovement != null)
            {
                botMovement.MoveSpeed = startSpeed;
            }
        }

        private void EnableGravity()
        {
            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }
        }
    }
}