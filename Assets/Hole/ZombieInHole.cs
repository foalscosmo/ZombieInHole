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
        [SerializeField] private AudioSource audioSource;
        public event Action<GameObject> OnZombieKilled;
        private void Start()
        {
            startSpeed = botMovement.MoveSpeed;
        }
        
        private bool hasTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!hasTriggered)
                {
                    rb.useGravity = false;
                    StartCoroutine(Timer());
                    hasTriggered = true;
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerNose"))
            {
                botMovement.MoveSpeed = 0;
            }
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            rb.useGravity = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                rb.useGravity = true;
                hasTriggered = false;
            }
            
            if (other.gameObject.layer == LayerMask.NameToLayer("PlayerNose"))
            {
                botMovement.MoveSpeed = startSpeed;
                audioSource.Play();
                OnZombieKilled?.Invoke(this.gameObject);
                StartCoroutine(DeathTimer());
            }
        }
        private IEnumerator DeathTimer()
        {
            yield return new WaitForSecondsRealtime(1f);
            gameObject.SetActive(false);
        }
    }
}