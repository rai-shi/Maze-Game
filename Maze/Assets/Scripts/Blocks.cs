using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{  
    private bool isCollided = false;
    void Start()
    {
    } 
    void Update() 
    {
    }
		private void OnCollisionEnter( Collision collision )
		{
      if(collision.gameObject.tag == "Player")
      {
        if(isCollided==false)
        {
          print(collision.gameObject.name); // terminalde her çarpışmad aplayer yazsını göreceksin
          GetComponent<MeshRenderer>().material.color = Color.red; 
          FindObjectOfType<Score_Manager>().score -= 3;
          isCollided = true; // böylece ikinci kez aynı engele çarptığında bir daha puan kesilmeyecek
        }
      }
		}
}
