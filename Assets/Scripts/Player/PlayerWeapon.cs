using System;
using AI;
using UnityEngine;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private LayerMask botBody;
        [SerializeField] private int value;
        public event Action<float> OnBotKill;
   
        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & botBody) != 0)
            {
                //var amount = other.GetComponent<BotScore>();
                // Debug.Log(amount.CurrentScore); // davamato tu ara es meqanika ? 
                OnBotKill?.Invoke(value);
            }
        }
    }
}