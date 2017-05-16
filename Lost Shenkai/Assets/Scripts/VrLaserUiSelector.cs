using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes it so the SteamVR_Laser can interact with UI elements
[RequireComponent (typeof(SteamVR_LaserPointer))]
public class VrLaserUiSelector : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
//	private SteamVR_Controller.Device Controller
//	{
//		get { return SteamVR_Controller.Input((int)trackedObj.index); }
//	}

	SteamVR_LaserPointer laser;
	private VrUiButton currentButton;

	void Awake () {
		laser = GetComponent<SteamVR_LaserPointer> ();
		laser.PointerIn += OnPointerIn;
		laser.PointerOut += OnPointerOut;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Game.platform == GamePlatform.VR_Oculus) {
			if (VrInput.GetTriggerDown(VrInput.ControllerSide.Either)) {
				if (currentButton != null) {
					currentButton.Select ();
				}
			}
		} else if (trackedObj != null && trackedObj.enabled == true) {
			if (VrInput.GetTriggerDown(VrInput.ControllerSide.Either)) {
				if (currentButton != null) {
					currentButton.Select ();
				}
			}
		}
	}

	void OnPointerIn (object sender, PointerEventArgs e) {
		currentButton = null;

		VrUiButton uiButton = e.target.GetComponent<VrUiButton> ();
		if (uiButton != null) {
			uiButton.Highlight ();
			currentButton = uiButton;
		} 

		// if UI
		if (e.target.gameObject.layer == 5) {
			laser.pointer.SetActive (true);
		} else {
			laser.pointer.SetActive (false);
		}
	}

	void OnPointerOut (object sender, PointerEventArgs e) {
		VrUiButton uiButton = e.target.GetComponent<VrUiButton> ();
		if (uiButton != null) {
			uiButton.Unhighlight ();
		}

		// if UI
		if (e.target.gameObject.layer == 5) {
			laser.pointer.SetActive (false);
			currentButton = null;
		}
	}
}
