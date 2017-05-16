using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSkybox : MonoBehaviour {

	public static GameSkybox current;
	public static float skyboxExposureDefault = 1.2f;

	public Material skyboxMat;
	private float rotationToAchieve;

	private float rotationSpeed = 0.2f;

	// Use this for initialization
	void Awake () {
		current = this;
	}
	
	// Update is called once per frame
	void Update () {

		RotateSkybox(rotationSpeed * Time.deltaTime);

		float curRot = skyboxMat.GetFloat ("_Rotation");
		float nextRot = Mathf.Lerp (curRot, rotationToAchieve, 5f * Time.deltaTime);
		skyboxMat.SetFloat ("_Rotation", nextRot);
	}

	public void SetExposure (float exp) {
		skyboxMat.SetFloat ("_Exposure", exp);
	}

	public float GetExposure() {
		return skyboxMat.GetFloat ("_Exposure");
	}

	public void RotateSkybox (float degrees) {
		rotationToAchieve += degrees;
	}
}
