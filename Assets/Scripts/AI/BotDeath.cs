using System;
using System.Collections;
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

        private readonly List<GameObject> _fruitPool = new();
        private int _fruitAmount;
        private Transform _fruitContainer;

        private void Start()
        {
            _fruitContainer = new GameObject(gameObject.name + " FruitContainer").transform;
            InitializeFruitPool();
        }

        private void InitializeFruitPool()
        {
            for (int i = 0; i < initialFruitPoolSize; i++)
            {
                GameObject randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Count)];
                GameObject fruitInstance = Instantiate(randomFruit, _fruitContainer);
                fruitInstance.SetActive(false);
                _fruitPool.Add(fruitInstance);
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
            _fruitAmount = Mathf.RoundToInt(botScore.CurrentScore);
            Vector3 deathPosition = transform.position;
            OnBotDeath?.Invoke(gameObject);
            DropFruits(deathPosition, _fruitAmount);
            gameObject.SetActive(false);
        }

        private void DropFruits(Vector3 botPosition, int fruitAmount)
        {
            for (int i = 0; i < fruitAmount; i++)
            {
                GameObject fruit = GetFruitFromPool();
                if (fruit != null)
                {
                    Vector3 randomDirection = Random.onUnitSphere;
                    float radius = 1f; 
                    fruit.transform.position = botPosition + randomDirection * radius; 
                    fruit.SetActive(true);
                    fruit.GetComponent<DroppableFruit>().Throw(botPosition);
                }
            }
        }

        private GameObject GetFruitFromPool()
        {
            foreach (GameObject fruit in _fruitPool)
            {
                if (!fruit.activeInHierarchy)
                    return fruit;
            }

            return null;
        }
    }
}