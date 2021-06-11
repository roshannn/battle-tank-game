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
    Coroutine coroutine;
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
        CreateWave(wave);
    }

    private void InitialiseEnemy()
    {
        EnemyService.Instance.GetEnemyTankType();
        EnemyService.Instance.AssignEnemyTransforms();
    }

    async private void CreateWave(int wave)
    {
        noOfEnemies = GetNumberOfEnemies(wave);
        spawnTransformPoints = GetSpawnPoints();
        for (int i = 0; i < noOfEnemies; i++)
        {
            int noOfSpawnPoints = spawnTransformPoints.Count;
            int j = i % noOfSpawnPoints;
            EnemyService.Instance.CreateEnemy(spawnTransformPoints[j]);
            await new WaitForSeconds(5);
        }
        while (noOfEnemies != 0)
        {
            continue;
        }
        int nextWave = wave + 1;
        waveOverText.text = "Wave " + wave + " Complete";
        waveOverTextObject.SetActive(true);
        await new WaitForSeconds(3);
        waveOverTextObject.SetActive(false);
        nextWaveText.text = "Wave " + nextWave + " Starting";
        nextWaveTextObject.SetActive(true);
        await new WaitForSeconds(3);
        nextWaveTextObject.SetActive(false);
        tankService.DestroyTank();
        NextWave();
    }

    private void NextWave()
    {
        StopCoroutine(coroutine);
        wave++;
        InitialiseEnemy();
        tankService.StartTank();
        CreateWave(wave);
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
