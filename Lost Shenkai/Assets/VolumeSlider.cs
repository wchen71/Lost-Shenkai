using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

	private Slider slider;

	private void Awake() {
		//slider = transform.parent.gameObject.GetComponent<Slider> ();
		slider = GetComponent<Slider> ();
	}

	private void Start() {
		slider.value = AudioListener.volume;
	}

	//Change the volume according to the slider value.
	public void SubmitVolume() {
		AudioListener.volume = slider.value;
		Debug.Log ("slider value is now" + slider.value);
	}
}
