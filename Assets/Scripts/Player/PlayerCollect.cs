using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollect : MonoBehaviour
    {
        [SerializeField] private LayerMask collectable;
        [SerializeField] private LayerMask smallCollectable;
        [SerializeField] private LayerMask bigCollectables;
        [SerializeField] private GameObject floatingScoringText;
        [SerializeField] private float fruitValue;
        [SerializeField] private float smallDestructibleValue;
        [SerializeField] private float bigDestructibleValue;
        
        public event Action<float> OnCollectFruit;
        public event Action<float> OnDestructibleCollect;
        public event Action<float> OnBotCollect;

        [SerializeField] private PlayerWeapon playerWeapon;

        private void OnEnable()
        {
            playerWeapon.OnBotKill += HandleBotKilled;
        }

        private void OnDisable()
        {
            playerWeapon.OnBotKill += HandleBotKilled;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & collectable) != 0)
            {
                InstantiateTextWhenCollect(fruitValue);
                OnCollectFruit?.Invoke(fruitValue);
            }
        
            if (((1 << other.gameObject.layer) & smallCollectable) != 0)
            {
                InstantiateTextWhenCollect(smallDestructibleValue);
                OnDestructibleCollect?.Invoke(smallDestructibleValue);
            }

            if (((1 << other.gameObject.layer) & bigCollectables) != 0)
            {
                InstantiateTextWhenCollect(bigDestructibleValue);
                OnDestructibleCollect?.Invoke(bigDestructibleValue);
            }
        }

        private void InstantiateTextWhenCollect(float value)
        {
            var transform1 = transform;
            var text = Instantiate(floatingScoringText, transform1.position, Quaternion.identity,transform1);
            text.GetComponent<TextMesh>().text = "+" +value;
        }

        private void HandleBotKilled(float value)
        {
            OnBotCollect?.Invoke(value);
            InstantiateTextWhenCollect(value);
        }
    }
}