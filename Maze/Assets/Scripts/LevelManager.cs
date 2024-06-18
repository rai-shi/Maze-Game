using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class LevelManager : MonoBehaviour
{

    
    private float[] levelTimes;
    private int levelTimeIncrease = 15;
    TimeManager TimeManagerScript;
 


    void Start()
    {
        
        TimeManagerScript = FindObjectOfType<TimeManager>();

        int sceneCount = SceneManager.sceneCountInBuildSettings; // level sayısını al
        levelTimes = new float[sceneCount]; // level sayısı kadar alan aç

        float currentTime = 20f;

        // level zamanlarını doldur
        for (int i = 0; i < sceneCount; i++)
        {
            levelTimes[i] = currentTime;
            currentTime += levelTimeIncrease;
        }

        // level zamanını ver
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (!(currentSceneIndex==0)) // 0 olan entry scene
        {
            float levelTime = levelTimes[currentSceneIndex];
            TimeManagerScript.SetLevelTime(levelTime);
        }
    }
    void Update()
    {}

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        // şu an aktif olan sahnenin index numarası alındı ve +1 eklendi yani bir sonraki sahnenin indexi
        
        int sceneIndex = SceneManager.sceneCountInBuildSettings -1; // toplam scene sayısı -1, indexlemek için

        if(nextSceneIndex <= sceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      
    }




}


