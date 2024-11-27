using System;
using UnityEngine;

namespace AI
{
    public class BotCollect : MonoBehaviour
    {
        [SerializeField] private LayerMask smallCollectable;
        [SerializeField] private LayerMask mediumCollectable;
        [SerializeField] private LayerMask bigCollectables;
        [SerializeField] private float smallCollectablesValue;
        [SerializeField] private float mediumCollectablesValue;
        [SerializeField] private float bigCollectableValue;
        public event Action<float> OnCollectFruit;
        public event Action<float> OnDestructibleCollect;
        public event Action<float> OnPlayerCollect;

        [SerializeField] private BotWeapon botWeapon;

        private void OnEnable()
        {
            botWeapon.OnPlayerKillHandleScale += HandlePlayerKilled;
        }

        private void OnDisable()
        {
            botWeapon.OnPlayerKillHandleScale -= HandlePlayerKilled;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & smallCollectable) != 0)
            {
                OnCollectFruit?.Invoke(smallCollectablesValue);
            }
        
            if (((1 << other.gameObject.layer) & mediumCollectable) != 0)
            {
                OnDestructibleCollect?.Invoke(mediumCollectablesValue);
            }

            if (((1 << other.gameObject.layer) & bigCollectables) != 0)
            {
                OnDestructibleCollect?.Invoke(bigCollectableValue);
            }
        }


        private void HandlePlayerKilled(float value)
        {
            OnPlayerCollect?.Invoke(value);
        }
    }
}