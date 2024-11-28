using UnityEngine;

namespace Hole
{
    public class SoundCollider : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource0;
        [SerializeField] private AudioClip zombieKillSound;

        [SerializeField] private AudioSource audioSource1;
        [SerializeField] private AudioClip buildingKillSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag($"Zombie"))
            {
                audioSource0.clip = zombieKillSound;
                audioSource0.Play();
            }
            
            if (other.gameObject.CompareTag($"Building"))
            {
                audioSource1.clip = buildingKillSound;
                audioSource1.Play();
            }
        }
    }
}