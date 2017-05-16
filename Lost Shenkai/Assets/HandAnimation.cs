using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour {

	public VrInput.ControllerSide side;
	Animator anim;

	private bool triggerDown = false;
	private bool gripDown = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool ("HandClosed", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (VrInput.GetTriggerDown (side)) {
			triggerDown = true;
		}
		if (VrInput.GetGripDown (side)) {
			gripDown = true;
		}

		if (VrInput.GetTriggerUp (side)) {
			triggerDown = false;
		}
		if (VrInput.GetGripUp (side)) {
			gripDown = false;
		}

		if (triggerDown || gripDown) {
			anim.SetBool ("HandClosed", true);
		} else {
			anim.SetBool ("HandClosed", false);
		}
	}
}
