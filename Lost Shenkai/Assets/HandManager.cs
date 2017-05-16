using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {

	public VrInput.ControllerSide side;
	public GameObject hitParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnHitParticles () {
		hitParticles.SetActive (false);
		hitParticles.SetActive (true);
	}

	public void VibrateController (float sec, float strength) {
		VrInput.ControllerVibrate (side, sec, strength);
	}
}
