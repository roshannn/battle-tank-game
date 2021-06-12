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
    public int enemiesLeft;
    private bool checkForLevelCompletion = false;
    public SceneLoader sceneLoader;
    async private void Start()
    {
        waveOverText = waveOverTextObject.GetComponent<Text>();
        nextWaveText = nextWaveTextObject.GetComponent<Text>();
        wave = 1;
        nextWaveText.text = "Wave " + wave + " Starting";
        nextWaveTextObject.SetActive(true);
        await new WaitForSeconds(2);
        nextWaveTextObject.SetActive(false);
        await new WaitForSeconds(1);
        InitialiseEnemy();
        tankService.StartTank();
        await new WaitForSeconds(3);
        StartCoroutine(CreateWave(wave));
    }
    private void Update()
    {
        if (enemiesLeft == 0&& checkForLevelCompletion)
        {
            EventService.Instance.allEnemiesDead += PreNextWave;
        }
    }

    async private void PreNextWave()
    {
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

    private void InitialiseEnemy()
    {
        EnemyService.Instance.GetEnemyTankType();
        EnemyService.Instance.AssignEnemyTransforms();
    }

    private IEnumerator CreateWave(int wave)
    {
        checkForLevelCompletion = false;
        noOfEnemies = GetNumberOfEnemies(wave);
        enemiesLeft = noOfEnemies;
        spawnTransformPoints = GetSpawnPoints();
        for (int i = 0; i < noOfEnemies; i++)
        {
            int noOfSpawnPoints = spawnTransformPoints.Count;
            int j = i % noOfSpawnPoints;
            EnemyService.Instance.CreateEnemy(spawnTransformPoints[j]);
            yield return new WaitForSeconds(5);
        }

        while(noOfEnemies != 0)
        {
            yield return null;
        }
        PreNextWave();
    }

    private void NextWave()
    {
        wave++;
        InitialiseEnemy();
        tankService.StartTank();
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

    public void GameOverScene()
    {
        sceneLoader.LoadGameOverScene();
    }
}
