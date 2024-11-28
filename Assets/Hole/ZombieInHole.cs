using System;
using System.Collections;
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

        private void Start()
        {
            if (botMovement != null) startSpeed = botMovement.MoveSpeed;
        }

        private bool hasTriggered;

        private void OnTriggerEnter(Collider other)
        {
            test = true;
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerNose"))
            {
                if (botMovement != null) botMovement.MoveSpeed = 0;
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!hasTriggered)
                {
                    rb.useGravity = false;
                    StartCoroutine(Timer());
                    hasTriggered = true;
                }
            }
        }

        private bool test;
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && test)
            {
                rb.useGravity = true;
                hasTriggered = false;
                test = false;
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            rb.useGravity = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerNose"))
            {
                if (botMovement != null) botMovement.MoveSpeed = startSpeed;
                OnZombieKilled?.Invoke(this.gameObject);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                rb.useGravity = true;
                hasTriggered = false;
            }
        }
    }
}