using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoSingletonGeneric<GameService>
{
    public int noOfEnemies;
    public GameObject waveOverTextObject;
    private Text waveOverText;
    public GameObject nextWaveTextObject;
    private Text nextWaveText;
    public Transform parent;
    public int wave;

    public EnemyService enemyService;
    public TankService tankService;
    private List<Transform> spawnTransformPoints;
    Coroutine createWave;
    private void Start()
    {
        waveOverText = waveOverTextObject.GetComponent<Text>();
        nextWaveText = nextWaveTextObject.GetComponent<Text>();
        wave = 1;
        InitialiseEnemy();
        tankService.StartTank();
        StartCoroutine(CreateWave(wave));
    }

    private void InitialiseEnemy()
    {
        enemyService.GetEnemyTankType();
        enemyService.AssignEnemyTransforms();
    }

    private IEnumerator CreateWave(int wave)
    {
        TankController tankController = FindObjectOfType<TankController>();
        noOfEnemies = GetNumberOfEnemies(wave);
        spawnTransformPoints = GetSpawnPoints();
        for (int i = 0; i < noOfEnemies; i++)
        {
            int noOfSpawnPoints = spawnTransformPoints.Count;
            int j = i % noOfSpawnPoints;
            GameObject go = Instantiate(enemyService.enemyType.tankPref, parent ,true);
            go.transform.position = spawnTransformPoints[j].position;
            go.transform.rotation = Quaternion.identity;
            
            EnemyController enemyController = go.GetComponent<EnemyController>();
            enemyController.InitializeValues(enemyService.enemyType);
            yield return new WaitForSeconds(3);
        }
        while (noOfEnemies != 0)
        {
            yield return null;
        }
        int nextWave = wave + 1;
        waveOverText.text = "Wave " + wave + " Complete";
        waveOverTextObject.SetActive(true);
        yield return new WaitForSeconds(3);
        waveOverTextObject.SetActive(false);
        nextWaveText.text = "Wave " + nextWave + " Starting";
        nextWaveTextObject.SetActive(true);
        yield return new WaitForSeconds(3);
        nextWaveTextObject.SetActive(false);

        tankService.DestroyTank();
        NextWave();
    }

    private void NextWave()
    {
        wave++;
        InitialiseEnemy();
        tankService.CreateTank();
        StartCoroutine(CreateWave(wave));
    }

    public List<Transform> GetSpawnPoints()
    {
        spawnTransformPoints = enemyService.enemyTransformScriptable.GetSpawnPoints();
        return spawnTransformPoints;
    }
    private int GetNumberOfEnemies(int wave)
    {
        return (wave * 2) + (wave / 2);
    }
}
