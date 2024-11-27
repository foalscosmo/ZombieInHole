using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using Scriptable_Obj;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hole
{
    public class SpherePoolManager : MonoBehaviour
    {
        [SerializeField] private List<BotData> botTypes = new();
        [SerializeField] private int poolSize;
        [SerializeField] private Vector2 spawnAreaMin;
        [SerializeField] private Vector2 spawnAreaMax;
        [SerializeField] private float botsMinSize;
        [SerializeField] private float botsMaxSize;
        [SerializeField] private int respawnBotTime;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private List<GameObject> botPool = new();
        
        private void Start()
        {
            InitializePool();
            SpawnBotsOnStart();
            foreach (var botDeath in botPool.Select(bot => bot.GetComponent<ZombieInHole>())
                         .Where(botDeath => botDeath != null)) botDeath.OnZombieKilled += HandleBotCollected;
        }
        

        private void InitializePool()
        {
            for (var i = 0; i < poolSize; i++)
            {
                var botData = botTypes[Random.Range(0, botTypes.Count)];
                var botInstance = Instantiate(botData.botObj);

                var randomSize = Random.Range(botsMinSize, botsMaxSize);
                botInstance.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                botInstance.SetActive(false);
                botPool.Add(botInstance);
            }
        }

        private void SpawnBotsOnStart()
        {
            foreach (var bot in botPool)
            {
                if (bot.activeInHierarchy) continue;
                var randomPosition = GetRandomPosition();
                bot.transform.position = randomPosition;
                bot.SetActive(true);
            }
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 randomPosition;

            do
            {
                randomPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    10f,
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );
            } 
            while (IsPositionOccupiedByPlayer(randomPosition));

            return randomPosition;
        }

        private bool IsPositionOccupiedByPlayer(Vector3 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, 0.5f, playerLayer);
            return colliders.Length > 0;
        }

        private void HandleBotCollected(GameObject collectedBot)
        {
            StartCoroutine(RespawnBotAfterDelay(collectedBot, respawnBotTime));
        }

        private IEnumerator RespawnBotAfterDelay(GameObject bot, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnBotToPool(bot);
        }

        private void ReturnBotToPool(GameObject bot)
        {
            bot.SetActive(false);
            var randomSize = Random.Range(botsMinSize, botsMaxSize);
            bot.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            var randomPosition = GetRandomPosition();
            bot.transform.position = randomPosition;
            bot.SetActive(true);
        }
    }
}