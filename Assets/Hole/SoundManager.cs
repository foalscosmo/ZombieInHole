using UnityEngine;

namespace Hole
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private KillZombie killZombie;
        [SerializeField] private AudioSource botKillSource;
        [SerializeField] private AudioClip[] soundClip; 

        
        private void OnEnable()
        {
            killZombie.OnZombieKill += SetBotKillSound;
        }

        private void OnDisable()
        {
            killZombie.OnZombieKill -= SetBotKillSound;
        }
        
        private void SetBotKillSound(int valueForEvent)
        {
            PlayKillSound(valueForEvent);
        }

        private void PlayKillSound(int index)
        {
            botKillSource.clip = soundClip[index];
            botKillSource.Play();
        }
    }
}