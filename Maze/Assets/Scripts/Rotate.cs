using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	// [SerializeField] private float x;
	// [SerializeField] private float y;
	// [SerializeField] private float z;
	[SerializeField] private Vector3 angle;

	void Start()
	{

	}
	void Update()
	{
		// transform.Rotate(x,y,z);
		// transform.Rotate(Vector3.up, Space.World) ile 0,1,0 değerleri çalıştırılır
	  transform.Rotate(angle, Space.World); // inspectorda y:1 yap
	}
}