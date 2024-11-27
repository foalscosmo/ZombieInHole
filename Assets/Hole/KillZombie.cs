using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hole
{
    public class KillZombie : MonoBehaviour
    {
        [SerializeField] private LayerMask botBody;
        [SerializeField] private GameObject floatingText;
        public int ZombieValue { get; private set; }

        public event Action<float> OnZombieKill;
        public event Action<int> OnZombieKillInts; 

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & botBody) != 0)
            {
                ZombieValue = Mathf.RoundToInt(other.transform.localScale.x);
                OnZombieKillInts?.Invoke(ZombieValue);
                InstantiateTextWhenCollect(ZombieValue);
                OnZombieKill?.Invoke(Mathf.RoundToInt(1));
            }
        }
        
        private void InstantiateTextWhenCollect(float value)
        {
            var transform1 = transform;
            var text = Instantiate(floatingText, transform1.position, Quaternion.identity,transform1);
            text.GetComponent<TextMeshPro>().text = "+" +value;
        }
    }
}