using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour {

	public GvrAudioSource mainTheme;
	public GvrAudioSource darkTheme;

	public float switchSpeed;

	public static float mainThemeVolume = 1.0f;
	public static float darkThemeVolume = 0.0f;

	void Awake() {
		mainThemeVolume = 1.0f;
		darkThemeVolume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainTheme.volume != mainThemeVolume) {
			mainTheme.volume = Mathf.Lerp (mainTheme.volume, mainThemeVolume, switchSpeed);
		}
		if (darkTheme.volume != darkThemeVolume) {
			darkTheme.volume = Mathf.Lerp (darkTheme.volume, darkThemeVolume, switchSpeed);
		}
	}

	public static void SwitchTracks() {
		float temp = mainThemeVolume;
		mainThemeVolume = darkThemeVolume;
		darkThemeVolume = temp;
	}

	public static bool IsMainActive() {
		return mainThemeVolume > darkThemeVolume;
	}
}