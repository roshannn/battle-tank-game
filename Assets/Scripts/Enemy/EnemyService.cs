using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    
    public EnemyScriptable[] enemyList;
    public EnemyScriptable enemyType;
    public List<EnemyTransformScriptable> enemyTransformScriptableList;
    public EnemyTransformScriptable enemyTransformScriptable;
    public List<Transform> spawnTransformPoints = new List<Transform>();
    public List<GameObject> enemyTankList;
   
    public void GetEnemyTankType()
    {
        int rand = Random.Range(0, enemyList.Length);
        enemyType = enemyList[rand];
        Debug.Log("enemy Type" + enemyType.enemyType);
    }
    

    public void AssignEnemyTransforms()
    {
        int rand = Random.Range(0, enemyTransformScriptableList.Count);
        enemyTransformScriptable = enemyTransformScriptableList[rand];
        SetYPosZero();
    }

    private void SetYPosZero()
    {
        for (int i = 0; i < spawnTransformPoints.Count; i++)
        {
            spawnTransformPoints[i].position = new Vector3(spawnTransformPoints[i].position.x, 0, spawnTransformPoints[i].position.z);
        }
    }

    internal void CreateEnemy(Transform spawnPoint)
    {
        GameObject go = Instantiate(enemyType.tankPref, spawnPoint.position, Quaternion.identity);
        EnemyController enemyController = go.GetComponent<EnemyController>();
        enemyController.InitializeValues(enemyType);
        enemyTankList.Add(go);
    }
}
