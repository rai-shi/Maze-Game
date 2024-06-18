 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float speed = 3f;
	[SerializeField] private float stopDistance = 1f;

	void Start()
	{
		target = GameObject.FindWithTag("Player").GetComponent<Transform>();
		// print(GameObject.FindWithTag("Player").gameObject.name);
	}
	void Update()
	{
		if( target != null )
		{
			transform.LookAt(target); // selfobject'in z açısı targeta bakacak
			float distance = Vector3.Distance(transform.position, target.position);
			//print(distance);
			if( distance>stopDistance)
			{
				transform.position += transform.forward*speed*Time.deltaTime;
			}
		}
		}
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
				Destroy(collision.gameObject);
				TimeManager timeManager = FindObjectOfType<TimeManager>();
				timeManager.gameOver= true;
		}
	}
}