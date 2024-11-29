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

        public event Action<int> OnZombieKill;
        private void Awake()
        {
            InstantiateTextWhenCollect(0);
        }

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & botBody) != 0)
            {
                ZombieValue = Mathf.RoundToInt(other.transform.localScale.x);
                OnZombieKill?.Invoke(ZombieValue);
                InstantiateTextWhenCollect(ZombieValue);
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