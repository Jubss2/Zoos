using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;

   void Start()
    {
        grid = GetComponentInChildren<GridController>();
    }
    public void InitialiseObjectSpawner()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for(int i =0; i< randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.avaliablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.avaliablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.avaliablePoints.RemoveAt(randomPos);
            Debug.Log("Objeto");
        }   
    }
}
