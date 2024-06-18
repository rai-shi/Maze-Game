using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 movement;
    [SerializeField] private float speed = 100f;
		private Rigidbody rigidbody;  
    private TimeManager timeManager;
    
    void Start()
    {
      rigidbody = GetComponent<Rigidbody>();
      timeManager = FindObjectOfType<TimeManager>();
    } 
    void Update()
    {
      if(timeManager.gameOver == false && timeManager.gameFinished == false)
      {
        MoveThePlayer();
      }
      if(timeManager.gameOver||timeManager.gameFinished)
      {
        rigidbody.isKinematic = true;
      }  
    }

    void MoveThePlayer()
    {
        float x = Input.GetAxis("Horizontal")*speed*Time.deltaTime; 
        // x değişkenini horizontal tuşları ile değiştirilebilir yaptık.
        float z = Input.GetAxis("Vertical")*speed*Time.deltaTime;
        movement = new Vector3(x, 0.65f, z); 
				rigidbody.AddForce(movement);  
        //transform.position += movement; 
    }
}