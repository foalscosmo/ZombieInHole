using System;
using UnityEngine;

namespace AI
{
    public class BotCollect : MonoBehaviour
    {
        [SerializeField] private LayerMask collectable;
        [SerializeField] private LayerMask smallCollectable;
        [SerializeField] private LayerMask bigCollectables;
        [SerializeField] private float fruitValue;
        [SerializeField] private float smallDestructibleValue;
        [SerializeField] private float bigDestructibleValue;
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
            if (((1 << other.gameObject.layer) & collectable) != 0)
            {
                OnCollectFruit?.Invoke(fruitValue);
            }
        
            if (((1 << other.gameObject.layer) & smallCollectable) != 0)
            {
                OnDestructibleCollect?.Invoke(smallDestructibleValue);
            }

            if (((1 << other.gameObject.layer) & bigCollectables) != 0)
            {
                OnDestructibleCollect?.Invoke(bigDestructibleValue);
            }
        }


        private void HandlePlayerKilled(float value)
        {
            OnPlayerCollect?.Invoke(value);
        }
    }
}