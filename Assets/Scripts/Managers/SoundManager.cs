using Player;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private PlayerCollect playerCollect;
        [SerializeField] private AudioSource collectableSource;
        [SerializeField] private AudioSource botKillSource;
        [SerializeField] private AudioClip[] soundClips; 

        private void OnEnable()
        {
            playerCollect.OnCollectFruit += HandleSoundWhenCollect;
            playerCollect.OnDestructibleCollect += HandleSoundWhenCollect;
            playerCollect.OnBotCollect += PlayBotKillSound;
        }

        private void OnDisable()
        {
            playerCollect.OnCollectFruit -= HandleSoundWhenCollect;
            playerCollect.OnDestructibleCollect -= HandleSoundWhenCollect;
            playerCollect.OnBotCollect -= PlayBotKillSound;
        }

        private void HandleSoundWhenCollect(float index)
        {
            switch (index)
            {
                case 1f:
                    PlayCollectableSound(0); 
                    break;
                case 2f:
                    PlayCollectableSound(1); 
                    break;
                case 4f:
                    PlayCollectableSound(2);
                    break;
            }
        }

        private void PlayCollectableSound(int clipIndex)
        {
            if (clipIndex >= 0 && clipIndex < soundClips.Length)
            {
                collectableSource.clip = soundClips[clipIndex];
                collectableSource.Play();
            }
        }

        private void PlayBotKillSound(float valueForEvent)
        {
            botKillSource.clip = soundClips[3];
            botKillSource.Play();
        }
    }
}