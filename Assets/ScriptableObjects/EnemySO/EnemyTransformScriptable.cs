using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyTransformScriptableObject", menuName = "ScriptableObject/Enemy/NewEnemyTransformScriptableObject")]
public class EnemyTransformScriptable : ScriptableObject
{
    public GameObject spawnPrefab;
    private List<Transform> spawnPoints;
    public GameObject playerSpawnPrefab;
    public List<Transform> GetSpawnPoints()
    {
        var spawnPoints = new List<Transform>();
        foreach (Transform child in spawnPrefab.transform)
        {
            spawnPoints.Add(child);
        }
        return spawnPoints;
    }

  

    public int GetNoOfSpawnPoints()
    {
        var spawnPoints = new List<Transform>();
        foreach (Transform child in spawnPrefab.transform)
        {
            spawnPoints.Add(child);
        }
        return spawnPoints.Count;
    }

    public Transform GetPlayerTransform()
    {
        return playerSpawnPrefab.transform;
    }


}