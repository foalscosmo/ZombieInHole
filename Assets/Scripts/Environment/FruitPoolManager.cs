using System.Collections;
using System.Collections.Generic;
using Scriptable_Obj;
using UnityEngine;

namespace Environment
{
    public class FruitPoolManager : MonoBehaviour
    {
       [SerializeField] private List<FruitData> fruitTypes = new();
        [SerializeField] private int poolSize = 10; 
        [SerializeField] private Vector2 spawnAreaMin; 
        [SerializeField] private Vector2 spawnAreaMax; 

        private readonly List<GameObject> _fruitPool = new();

        private void OnEnable()
        {
            FruitCollector.OnFruitCollectedPlayer += DOHandleFruitCollectedPlayer;
            FruitCollector.OnFruitCollectedBot += DOHandleFruitCollectedPlayer;

        }

        private void OnDisable()
        {
            FruitCollector.OnFruitCollectedPlayer -= DOHandleFruitCollectedPlayer;
            FruitCollector.OnFruitCollectedBot -= DOHandleFruitCollectedPlayer;

        }

        private void Start()
        {
            InitializePool();
            SpawnFruits();
        }

        private void InitializePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                FruitData fruitData = fruitTypes[Random.Range(0, fruitTypes.Count)];
                GameObject fruitInstance = Instantiate(fruitData.fruitObj, transform);
                fruitInstance.SetActive(false); 
                _fruitPool.Add(fruitInstance); 
            }
        }

        private void SpawnFruits()
        {
            foreach (GameObject fruit in _fruitPool)
            {
                if (!fruit.activeInHierarchy)
                {
                    Vector3 spawnPosition = GetUniqueSpawnPosition();
                    fruit.transform.position = spawnPosition;
                    fruit.transform.localScale = new Vector3(4, 4, 4);
                    fruit.SetActive(true);
                }
            }
        }

        private void DOHandleFruitCollectedPlayer(GameObject collectedFruit)
        {
            StartCoroutine(RespawnFruitAfterDelay(collectedFruit, 10f));
        }

        private IEnumerator RespawnFruitAfterDelay(GameObject fruit, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnFruitToPool(fruit);
        }

        private void ReturnFruitToPool(GameObject fruit)
        {
            fruit.SetActive(false);
            fruit.transform.localScale = new Vector3(4, 4, 4);
            Vector3 respawnPosition = GetUniqueSpawnPosition();
            fruit.transform.position = respawnPosition;
            fruit.SetActive(true);
        }

        private Vector3 GetUniqueSpawnPosition()
        {
            Vector3 newPosition;
            bool positionIsUnique;

            do
            {
                newPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    1f, 
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                positionIsUnique = true;

                // Check if this position overlaps with any active fruit
                foreach (var fruit in _fruitPool)
                {
                    if (fruit.activeInHierarchy && Vector3.Distance(newPosition, fruit.transform.position) < 0.1f)
                    {
                        positionIsUnique = false;
                        break;
                    }
                }

            } while (!positionIsUnique); // Keep generating a new position until it's unique

            return newPosition;
        }
    }
}