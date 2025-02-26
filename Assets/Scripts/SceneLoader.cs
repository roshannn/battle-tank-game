﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
