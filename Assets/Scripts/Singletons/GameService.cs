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
        coroutine = StartCoroutine(CreateWave(wave));
    }

    private void InitialiseEnemy()
    {
        EnemyService.Instance.GetEnemyTankType();
        EnemyService.Instance.AssignEnemyTransforms();
    }

    private IEnumerator CreateWave(int wave)
    {
        noOfEnemies = GetNumberOfEnemies(wave);
        spawnTransformPoints = GetSpawnPoints();
        for (int i = 0; i < noOfEnemies; i++)
        {
            int noOfSpawnPoints = spawnTransformPoints.Count;
            int j = i % noOfSpawnPoints;
            EnemyService.Instance.CreateEnemy(spawnTransformPoints[j]);
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
        StopCoroutine(coroutine);
        wave++;
        InitialiseEnemy();
        tankService.StartTank();
        coroutine = StartCoroutine(CreateWave(wave));
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
