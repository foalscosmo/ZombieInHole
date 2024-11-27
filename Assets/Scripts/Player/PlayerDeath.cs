using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public event Action OnPlayerDeath;

        [SerializeField] private List<GameObject> fruitPrefabs;
        [SerializeField] private int initialFruitPoolSize;
        [SerializeField] private LayerMask weapon;
        [SerializeField] private PlayerScore playerScore;

        private readonly List<DroppableFruit> droppableFruits = new();
        
        private readonly List<GameObject> fruitPool = new();
        private int fruitAmount;
        private Transform fruitContainer;
        private bool isDead;

        private void Start()
        {
            fruitContainer = new GameObject("Player FruitContainer").transform;
            InitializeFruitPool();
        }

        private void InitializeFruitPool()
        {
            for (int i = 0; i < initialFruitPoolSize; i++)
            {
                var randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Count)];
                var fruitInstance = Instantiate(randomFruit, fruitContainer); 
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
            fruitAmount = Mathf.RoundToInt(playerScore.CurrentScore);
            var deathPosition = transform.position;
            OnPlayerDeath?.Invoke();
            DropFruits(deathPosition, fruitAmount);
            gameObject.SetActive(false);
        }

        private void DropFruits(Vector3 botPosition, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var fruit = GetFruitFromPool();
                if (fruit != null)
                {
                    var randomDirection = Random.onUnitSphere;
                    var radius = 1f; 
                    fruit.transform.position = botPosition + randomDirection * radius; 
                    fruit.SetActive(true);
                    
                    var droppableFruit = droppableFruits[fruitPool.IndexOf(fruit)];
                    droppableFruit.Throw(botPosition);
                }
            }
        }

        private GameObject GetFruitFromPool()
        {
            foreach (var fruit in fruitPool)
            {
                if (!fruit.activeInHierarchy)
                    return fruit;
            }
            return null;
        }
    }
}