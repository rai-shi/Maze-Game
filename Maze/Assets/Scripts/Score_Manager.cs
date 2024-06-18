using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score_Manager: MonoBehaviour
{  
    public int score = 0; // her bloğa çarpıldığında score azaltılacak
    public TMP_Text scoreText;
    void Start()
    {
    } 
    void Update() 
    {
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
    scoreText.text = "Score: " +score.ToString();
    }
}
