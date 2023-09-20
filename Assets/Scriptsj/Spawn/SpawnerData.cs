using UnityEngine;

[CreateAssetMenu(fileName ="Spawner.asset", menuName = "Spawers/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject itemToSpawn;

    public int minSpawn;

    public int maxSpawn;

}
