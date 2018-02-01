using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnObjects> spawnableObjects;
    [HideInInspector]public List<Transform> spawnPoints;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            print(child.ToString());
            spawnPoints.Add(child);
        }
    }

    void Activate()
    {
        for (int i = 0; i < spawnableObjects.Count; i++)
        {
            for (int f = 0; f < spawnableObjects[i].amount; f++)
            {
                Instantiate(spawnableObjects[i].obj, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            }
        }
    }

    void Deactivate()
    {
        Debug.Log("Spawner doesn't have a Deactivate function");
    }
}

[System.Serializable]
public class SpawnObjects
{
    public GameObject obj;
    public int amount;
}