using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickable : MonoBehaviour
{
    public int normalScoreAmount = 2;
    public int turningScoreAmount = 5;
    public int bounceScoreAmount = 10;

    public AnalyticsManager analytics_manager;


    void Start()
    { 

    }


    void Update()
    { }


  private void OnTriggerEnter(Collider other)
  {
    if(other.tag=="Player")
    {
      Score_Manager scoreMan = FindObjectOfType<Score_Manager>();
      if (gameObject.tag == "TurningCoin")
      {
          scoreMan.score += turningScoreAmount;
      }
      else if (gameObject.tag == "Coin")
      {
          scoreMan.score += normalScoreAmount;

      }
      else
      {
          scoreMan.score += bounceScoreAmount;
          
      }
      Destroy(gameObject);

      analytics_manager.CollectCoin();
    }
  }


		private void OnCollisionEnter(Collision collision)
    {
    }

}