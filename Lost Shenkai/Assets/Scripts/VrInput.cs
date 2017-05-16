using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VrInput : MonoBehaviour {

	public enum ControllerSide {
		Left,
		Right,
		Either
	}

	private void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool GetStartDown (ControllerSide side) {
		if (Game.platform == GamePlatform.VR_Oculus) {
			return StartDownOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return StartDownVive (side);
		}
		return false;
	}

	public static bool GetTriggerDown (ControllerSide side) {
//		Debug.Log ("HEEYYYY");
//		if (!ControllerExists (side)) {
//			return false;
//		}

		if (Game.platform == GamePlatform.VR_Oculus) {
			return TriggerDownOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return TriggerDownVive (side);
		}
		return false;
	}

	public static bool GetTriggerUp (ControllerSide side) {
//		if (!ControllerExists (side)) {
//			return false;
//		}

		if (Game.platform == GamePlatform.VR_Oculus) {
			return TriggerUpOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return TriggerUpVive (side);
		}
		return false;
	}

	public static bool GetGripDown (ControllerSide side) {
		//		Debug.Log ("HEEYYYY");
		//		if (!ControllerExists (side)) {
		//			return false;
		//		}

		if (Game.platform == GamePlatform.VR_Oculus) {
			return GripDownOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return GripDownVive (side);
		}
		return false;
	}

	public static bool GetGripUp (ControllerSide side) {
		//		if (!ControllerExists (side)) {
		//			return false;
		//		}

		if (Game.platform == GamePlatform.VR_Oculus) {
			return GripUpOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return GripUpVive (side);
		}
		return false;
	}

	public static bool ControllerExists (ControllerSide side) {
		if (Game.platform == GamePlatform.VR_Oculus) {
			return ControllerExistsOculus (side);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			return ControllerExistsVive (side);
		}
		return false;
	}

	public static void ControllerVibrate (ControllerSide side, float sec, float strength) {
		if (Game.platform == GamePlatform.VR_Oculus) {
			VibrateOculus (side, sec, strength);
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			VibrateVive (side, sec, strength);
		}
	}


	/** Private Helpers **/



	/***************
	* START BUTTON
	***************/

	private static bool StartDownOculus (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return OVRInput.GetDown (OVRInput.Button.Start);
		}
		if (side == ControllerSide.Right) {
			return OVRInput.GetDown (OVRInput.Button.Start);
		}
		if (side == ControllerSide.Either) {
			return OVRInput.GetDown (OVRInput.Button.Start) ||
				OVRInput.GetDown (OVRInput.Button.Start);
		}
		return false;
	}

	private static bool StartDownVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
		}
		if (side == ControllerSide.Either) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_ApplicationMenu) ||
				ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
		}
		return false;
	}


	/***************
	 * TRIGGER BUTTON
	 ***************/

	private static bool TriggerDownOculus (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return OVRInput.GetDown (OVRInput.RawButton.LIndexTrigger);
		}
		if (side == ControllerSide.Right) {
			return OVRInput.GetDown (OVRInput.RawButton.RIndexTrigger);
		}
		if (side == ControllerSide.Either) {
			return OVRInput.GetDown (OVRInput.RawButton.LIndexTrigger) ||
				OVRInput.GetDown (OVRInput.RawButton.RIndexTrigger);
		}
		return false;
	}

	private static bool TriggerDownVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		if (side == ControllerSide.Either) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) ||
				ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		return false;
	}
		
	private static bool TriggerUpOculus (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return OVRInput.GetUp (OVRInput.RawButton.LIndexTrigger);
		}
		if (side == ControllerSide.Right) {
			return OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger);
		}
		if (side == ControllerSide.Either) {
			return OVRInput.GetUp (OVRInput.RawButton.LIndexTrigger) ||
				OVRInput.GetUp (OVRInput.RawButton.RIndexTrigger);
		}
		return false;
	}

	private static bool TriggerUpVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		if (side == ControllerSide.Either) {
			return ViveControllerInputManager.leftManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) ||
				ViveControllerInputManager.rightManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		return false;
	}

	/***************
	 * GRIP BUTTON
	 ***************/

	private static bool GripDownOculus (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return OVRInput.GetDown (OVRInput.RawButton.LHandTrigger);
		}
		if (side == ControllerSide.Right) {
			return OVRInput.GetDown (OVRInput.RawButton.RHandTrigger);
		}
		if (side == ControllerSide.Either) {
			return OVRInput.GetDown (OVRInput.RawButton.LHandTrigger) ||
				OVRInput.GetDown (OVRInput.RawButton.RHandTrigger);
		}
		return false;
	}

	private static bool GripDownVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		if (side == ControllerSide.Either) {
			return ViveControllerInputManager.leftManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_Grip) ||
				ViveControllerInputManager.rightManager.GetTouchDown (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		return false;
	}

	private static bool GripUpOculus (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return OVRInput.GetUp (OVRInput.RawButton.LHandTrigger);
		}
		if (side == ControllerSide.Right) {
			return OVRInput.GetUp(OVRInput.RawButton.RHandTrigger);
		}
		if (side == ControllerSide.Either) {
			return OVRInput.GetUp (OVRInput.RawButton.LHandTrigger) ||
				OVRInput.GetUp (OVRInput.RawButton.RHandTrigger);
		}
		return false;
	}

	private static bool GripUpVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		if (side == ControllerSide.Either) {
			return ViveControllerInputManager.leftManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_Grip) ||
				ViveControllerInputManager.rightManager.GetTouchUp (Valve.VR.EVRButtonId.k_EButton_Grip);
		}
		return false;
	}

	/***************
	 * CONTROLLER EXISTS
	 ***************/

	private static bool ControllerExistsOculus (ControllerSide side) {
		string joystickName = "";
		if (side == ControllerSide.Left) {
			joystickName = "OpenVR Controller - Left";
		}
		if (side == ControllerSide.Right) {
			joystickName = "OpenVR Controller - Right";
		}

		foreach (string joystick in UnityEngine.Input.GetJoystickNames ()) {
			if (joystickName != "" && joystick == joystickName) {
				return true;
			}
		}
		return false;
	}

	private static bool ControllerExistsVive (ControllerSide side) {
		if (side == ControllerSide.Left) {
			return ViveControllerInputManager.leftManager.ControllerExists ();
		}
		if (side == ControllerSide.Right) {
			return ViveControllerInputManager.rightManager.ControllerExists ();
		}
		return false;
	}

	/***************
	 * VIBRATION
	 ***************/

	private static void VibrateOculus (ControllerSide side, float sec, float strength) {
		int length = Mathf.FloorToInt(320f * sec); // 320Hz Sample rate
		if (side == ControllerSide.Left) {
			OVRHapticsClip audioClip = CreateHapticsClipFading (length, strength);
			OVRHaptics.LeftChannel.Mix (audioClip);
		}
		if (side == ControllerSide.Right) {
			OVRHapticsClip audioClip = CreateHapticsClipFading (length, strength);
			OVRHaptics.RightChannel.Mix (audioClip);
		}
		if (side == ControllerSide.Either) {
			OVRHapticsClip audioClip = CreateHapticsClipFading (length, strength);
			OVRHaptics.LeftChannel.Mix (audioClip);
			OVRHaptics.RightChannel.Mix (audioClip);
		}
	}

	private static void VibrateVive (ControllerSide side, float sec, float strength) {
		int length = Mathf.FloorToInt(1000000f * sec); // in microSec
		if (side == ControllerSide.Left) {
			ViveControllerInputManager.leftManager.Vibrate (length);
		}
		if (side == ControllerSide.Right) {
			ViveControllerInputManager.rightManager.Vibrate (length);
		}
		if (side == ControllerSide.Either) {
			ViveControllerInputManager.leftManager.Vibrate (length);
			ViveControllerInputManager.rightManager.Vibrate (length);
		}
	}

	private static OVRHapticsClip CreateHapticsClip(int length, float strength) // strength is from 0 - 1
	{
		OVRHapticsClip finalClip;
		finalClip = new OVRHapticsClip (length);

		int finalStrength = Mathf.FloorToInt (Mathf.Clamp (strength * 255, 0, 255));

		for (int i = 0; i < length; i++)
		{
			finalClip.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)finalStrength;
		}

		return new OVRHapticsClip(finalClip.Samples, finalClip.Samples.Length);
	}

	private static OVRHapticsClip CreateHapticsClipFading(int length, float strength) // strength is from 0 - 1
	{
		OVRHapticsClip finalClip;
		finalClip = new OVRHapticsClip (length);

		int finalStrength = Mathf.FloorToInt (Mathf.Clamp (strength * 255, 0, 255));

		for (int i = 0; i < length; i++)
		{
			finalClip.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)(finalStrength - (finalStrength / length) * i);
		}

		return new OVRHapticsClip(finalClip.Samples, finalClip.Samples.Length);
	}

}
