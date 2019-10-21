using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParallel : MonoBehaviour
{

    public Honda.GameManager GM;
    public Transform[] spawnPoint;
    public Transform[] destinationPoint;
    public Transform spawnParent;
    public GameObject[] treeEntityPrefab;
    public float spawnInterval;

    int spawnNumber;

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

        GameObject newEntity;

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            newEntity = Instantiate(treeEntityPrefab[GenerateRandomNumber(spawnNumber, treeEntityPrefab)], spawnPoint[i].position, Quaternion.identity, spawnParent);
            AssignDestination(newEntity, destinationPoint[i]);
        }
        /*
        //Left tree
        newEntity = Instantiate(treeEntityPrefab[GenerateRandomNumber(spawnNumber, treeEntityPrefab)], spawnPoint[0].position, Quaternion.identity, spawnParent);
        AssignDestination(newEntity, destinationPoint[0]);

        //Right tree
        newEntity = Instantiate(treeEntityPrefab[GenerateRandomNumber(spawnNumber, treeEntityPrefab)], spawnPoint[1].position, Quaternion.identity, spawnParent);
        AssignDestination(newEntity, destinationPoint[1]);
        */


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
        objectMover.enabled = true;
    }
}
