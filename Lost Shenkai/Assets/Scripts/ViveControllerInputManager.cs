using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputManager : MonoBehaviour {

	public static ViveControllerInputManager leftManager;
	public static ViveControllerInputManager rightManager;

	public VrInput.ControllerSide side;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		if (side == VrInput.ControllerSide.Left) {
			leftManager = this;
		}
		if (side == VrInput.ControllerSide.Right) {
			rightManager = this;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool GetTouchDown (Valve.VR.EVRButtonId button) {
		if (controller == null) {
			return false;
		}
		return controller.GetTouchDown (button);
	}

	public bool GetTouchUp (Valve.VR.EVRButtonId button) {
		if (controller == null) {
			return false;
		}
		return controller.GetTouchUp (button);
	}

	public void Vibrate (int microSec) {
		controller.TriggerHapticPulse ((ushort)microSec);
	}

	public bool ControllerExists () {
		return controller.connected;
	}
}
