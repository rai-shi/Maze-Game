using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TimeManager : MonoBehaviour
{
    public bool gameFinished = false;
    private float levelFinishTime;
    private float currentLevelTime;
    public bool gameOver = false;
    [SerializeField] private TMP_Text timeText; 
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lostPanel;

    [SerializeField] private List<GameObject> destroyAfterGame = new List<GameObject>();


    void Start()
    {

        lostPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        
        UpdateObjectList("Coin");
        UpdateObjectList("TurningCoin");
        UpdateObjectList("Enemy");
    }
    void Update()
    { 


        if(gameFinished == false && gameOver == false)
        {
        UpdateTheTimer(); // oyun bitince süre durdurulmalı
        }

        if(Time.timeSinceLevelLoad >= levelFinishTime && gameOver == false )
        { // oyuncu yenilmemiş ve süre dolmuş ise
            gameFinished = true;
            lostPanel.gameObject.SetActive(false);
            winPanel.gameObject.SetActive(true);

            // coinleri yok etme
            foreach(GameObject allObjects in destroyAfterGame)//in GameObject.FindGameObjectsWithTag("Coin")
            {
                Destroy(allObjects);
            }
        }


        if(gameOver==true) // oyun kaybedilmiş ise
        {
            winPanel.gameObject.SetActive(false);
            lostPanel.gameObject.SetActive(true);
            // coinleri yok etme
            foreach(GameObject allObjects in destroyAfterGame)
            {
                Destroy(allObjects);
            }
        }
    }

    public void SetLevelTime(float time) // level manager da level time güncelleniyor
    {
        currentLevelTime = time;
        levelFinishTime = currentLevelTime;
    }

	private void UpdateTheTimer() 
	{
		if (currentLevelTime > 0)
        {
            currentLevelTime -= Time.deltaTime;
            timeText.text = "Time: " + Mathf.RoundToInt(currentLevelTime);
        }
	}


    private void UpdateObjectList(string tag)
    {
        destroyAfterGame.AddRange(GameObject.FindGameObjectsWithTag(tag));
    }
}