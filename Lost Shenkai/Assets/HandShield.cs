using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShield : MonoBehaviour {

	private float rotationSpeed = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, transform.up, rotationSpeed * Time.deltaTime);
	}
}
