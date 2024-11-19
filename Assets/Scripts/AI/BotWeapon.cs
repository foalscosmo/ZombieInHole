using System;
using UnityEngine;

namespace AI
{
    public class BotWeapon : MonoBehaviour
    {
        [SerializeField] private LayerMask playerBody;
        public event Action<float> OnPlayerKillHandleScale;
   
        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & playerBody) != 0)
            {
                OnPlayerKillHandleScale?.Invoke(8);
            }
        }
    }
}