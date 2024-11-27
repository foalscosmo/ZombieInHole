using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class BotDeath : MonoBehaviour
    {
        public event Action<GameObject> OnBotDeath;

        [SerializeField] private List<GameObject> fruitPrefabs;
        [SerializeField] private int initialFruitPoolSize = 300;
        [SerializeField] private LayerMask weapon;
        [SerializeField] private BotScore botScore;

        private readonly List<GameObject> fruitPool = new();
        private readonly List<DroppableFruit> droppableFruits = new();

        private int fruitAmount;
        private Transform fruitContainer;

        private void Start()
        {
            fruitContainer = new GameObject(gameObject.name + " FruitContainer").transform;
            InitializeFruitPool();
        }

        private void InitializeFruitPool()
        {
            for (int i = 0; i < initialFruitPoolSize; i++)
            {
                GameObject randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Count)];
                GameObject fruitInstance = Instantiate(randomFruit, fruitContainer);
                fruitInstance.SetActive(false);
                fruitPool.Add(fruitInstance);
                
                var droppableFruit = fruitInstance.GetComponent<DroppableFruit>();
                if (droppableFruit != null) droppableFruits.Add(droppableFruit);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & weapon) != 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            fruitAmount = Mathf.RoundToInt(transform.localScale.x * 30);
            Vector3 deathPosition = transform.position;
            OnBotDeath?.Invoke(gameObject);
            DropFruits(deathPosition, fruitAmount);
            gameObject.SetActive(false);
        }

        private void DropFruits(Vector3 botPosition, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject fruit = GetFruitFromPool();
                if (fruit != null)
                {
                    Vector3 randomDirection = Random.onUnitSphere;
                    fruit.transform.position = botPosition + randomDirection; 
                    fruit.SetActive(true);
                    
                    var droppableFruit = droppableFruits[fruitPool.IndexOf(fruit)];
                    droppableFruit.Throw(botPosition);
                }
            }
        }

        private GameObject GetFruitFromPool()
        {
            foreach (GameObject fruit in fruitPool)
            {
                if (!fruit.activeInHierarchy)
                    return fruit;
            }

            return null;
        }
    }
}