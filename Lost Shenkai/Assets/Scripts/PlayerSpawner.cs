using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	void Awake () {
		if (Game.platform == GamePlatform.VR_Oculus) {
			SpawnOculus ();
		}
		if (Game.platform == GamePlatform.VR_Vive) {
			SpawnVive ();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnOculus () {
		GameObject prefab = Resources.Load ("OVRPlayerController") as GameObject;
		Instantiate (prefab, transform.position, transform.rotation);
	}

	private void SpawnVive () {
		GameObject prefab = Resources.Load ("VivePlayer") as GameObject;
		Instantiate (prefab, transform.position, transform.rotation);
	}
}
