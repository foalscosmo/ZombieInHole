using System.Collections;
using System.Collections.Generic;
using Scriptable_Obj;
using UnityEngine;

namespace Environment
{
    public class FruitPoolManager : MonoBehaviour
    {
        [SerializeField] private List<FruitData> fruitTypes = new();
        [SerializeField] private int poolSize; 
        [SerializeField] private Vector2 spawnAreaMin; 
        [SerializeField] private Vector2 spawnAreaMax; 

        private readonly List<GameObject> fruitPool = new();

        private void OnEnable()
        {
            FruitCollector.OnFruitCollectedPlayer += DoHandleFruitCollectedPlayer;
            FruitCollector.OnFruitCollectedBot += DoHandleFruitCollectedPlayer;

        }

        private void OnDisable()
        {
            FruitCollector.OnFruitCollectedPlayer -= DoHandleFruitCollectedPlayer;
            FruitCollector.OnFruitCollectedBot -= DoHandleFruitCollectedPlayer;

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
                fruitPool.Add(fruitInstance); 
            }
        }

        private void SpawnFruits()
        {
            foreach (GameObject fruit in fruitPool)
            {
                if (!fruit.activeInHierarchy)
                {
                    Vector3 spawnPosition = GetUniqueSpawnPosition();
                    fruit.transform.position = spawnPosition;
                    fruit.transform.localScale = new Vector3(9,9,9);
                    fruit.SetActive(true);
                }
            }
        }

        private void DoHandleFruitCollectedPlayer(GameObject collectedFruit)
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
            fruit.transform.localScale = new Vector3(9,9,9);
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

                foreach (var fruit in fruitPool)
                {
                    if (fruit.activeInHierarchy && Vector3.Distance(newPosition, fruit.transform.position) < 0.1f)
                    {
                        positionIsUnique = false;
                        break;
                    }
                }

            } while (!positionIsUnique); 

            return newPosition;
        }
    }
}