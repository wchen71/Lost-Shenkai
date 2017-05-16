using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTextController : MonoBehaviour {

	private Text highScoreText;

	// Use this for initialization
	void Awake() {
		highScoreText = GetComponent<Text> ();
	}

	void OnEnable() {
		highScoreText.text = "High Score: " + Game.GetHighScore ().ToString ();
	}
}
