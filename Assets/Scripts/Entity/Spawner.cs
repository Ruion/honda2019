using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Honda.GameManager GM;
    public Transform[] spawnPoint;
    public Transform[] destinationPoint;
    public Transform spawnParent;
    public GameObject[] carEntityPrefab;
    public GameObject[] coinsPrefab;
    public float spawnInterval;

    int spawnNumber;
    int spawnPointNumber;
    int lastSpawnPointNumber;

    [Range(0, 1)]
    public float spawnCoinPercentage;
    [Range(0, 10f)]
    public float spawnCoinInterval = 2f;
    private bool coinsIsSpawnedRecently;

	public void StartGame()
    {
        StartCoroutine(StartSpawn());
    }

    public void StopGame()
    {
        StopAllCoroutines();
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);

        // Spawn new entity on another lane
        int newLane = GenerateRandomNumber(spawnPointNumber, spawnPoint);
        while ( newLane == lastSpawnPointNumber)
        {
            newLane = GenerateRandomNumber(spawnPointNumber, spawnPoint);
        }
        lastSpawnPointNumber = spawnPointNumber = newLane;

        GameObject newEntity;

        if (coinsIsSpawnedRecently)
        {
            // spawn car
            spawnNumber = GenerateRandomNumber(spawnNumber, carEntityPrefab);
            newEntity = Instantiate(carEntityPrefab[spawnNumber], spawnPoint[spawnPointNumber].position, Quaternion.identity, spawnParent);
            
        }
        else
        {
            bool spawnCoin = ShouldSpawnCoins();

            if (spawnCoin)
            {
                // spawn coin
                spawnNumber = GenerateRandomNumber(spawnNumber, coinsPrefab);
                newEntity = Instantiate(coinsPrefab[spawnNumber], spawnPoint[spawnPointNumber].position, Quaternion.identity, spawnParent);
                coinsIsSpawnedRecently = true;
                Invoke("ClearCoinsSpawnedInterval", spawnCoinInterval);
            }
            else
            {
                // spawn car
                spawnNumber = GenerateRandomNumber(spawnNumber, carEntityPrefab);
                newEntity = Instantiate(carEntityPrefab[spawnNumber], spawnPoint[spawnPointNumber].position, Quaternion.identity, spawnParent);
            }
        }

        

        AssignDestination(newEntity, destinationPoint[spawnPointNumber]);

        StartCoroutine(StartSpawn());

    }

    int GenerateRandomNumber(int spawnNumber_, Object[] objects)
    {
        int rand = Random.Range(0, objects.Length);

            return rand;
    }

    void AssignDestination(GameObject obj, Transform target)
    {
        ObjectMover objectMover = obj.GetComponentInChildren<ObjectMover>();
        objectMover.GM = GM;
        objectMover.target = target;
    }

    private bool ShouldSpawnCoins()
    {
        bool spawnCoins = false;

        float randNumber = Random.Range(0f, 1f);
        if (randNumber <= spawnCoinPercentage)
        {
            spawnCoins = true;
        }

        return spawnCoins;
    }

    private void ClearCoinsSpawnedInterval()
    {
        coinsIsSpawnedRecently = false;
    }
}
