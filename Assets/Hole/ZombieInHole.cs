using System.Collections;
using UnityEngine;

namespace Hole
{
    public class ZombieInHole : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //     {
        //         rb.useGravity = false;
        //         StartCoroutine(Timer());
        //     }
        // }
        //
        // private IEnumerator Timer()
        // {
        //     yield return new WaitForSecondsRealtime(0.1f);
        //     rb.useGravity = true;
        // }
        //
        // private void OnTriggerStay(Collider other)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //     {
        //         rb.useGravity = false;
        //         StartCoroutine(Timer());
        //     }
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //     {
        //         rb.useGravity = true;
        //     }
        // }
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
                hasTriggered = false; // Reset the flag to allow triggering again
            }
        }
    }
}