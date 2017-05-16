using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempUIManager : MonoBehaviour {

	public GameObject canvasAnchor;
	public Vector3 startPos;
	public float uiMoveSpeed;

	public Text scoreText;
	public Slider healthSlider;

	public GameObject gameOverUI;
	public GameObject waveBreakUI;
	public GameObject mainMenuPanel;
	public GameObject pauseMenuPanel;
	public GameObject highScorePanel;

	private static int score = 0;
	private static int health = 100;
	private static bool gameOverActive;
	private static bool waveBreakActive;

	public float inGameVolume;
	public float menuVolume;

	void Awake() {
		score = 0;
		health = Player.MAX_HEALTH;
		gameOverActive = false;
		waveBreakActive = false;
	}

	void Start() {
		gameOverUI.SetActive (false);
		waveBreakUI.SetActive (false);
		healthSlider.maxValue = health;

		transform.position = canvasAnchor.transform.position;
		transform.rotation = canvasAnchor.transform.rotation;
	}

	void Update() {
		scoreText.text = "Score: " + score;
		healthSlider.value = health;
		gameOverUI.SetActive (gameOverActive);
		waveBreakUI.SetActive (waveBreakActive);

		if (VrInput.GetStartDown (VrInput.ControllerSide.Either) && !this.mainMenuPanel.activeSelf && !this.highScorePanel.activeSelf && !this.gameOverUI.activeSelf) {
			this.SetHUD (false);
			Utilities.GetSpawner ().PauseMenu (true);
			SwitchToPauseMenuButton ();
			if (Soundtrack.IsMainActive ()) {
				Soundtrack.mainThemeVolume = menuVolume;
			} else {
				Soundtrack.darkThemeVolume = menuVolume;
			}
		}

		transform.position = Vector3.Lerp (transform.position, canvasAnchor.transform.position, uiMoveSpeed * Time.deltaTime);
		transform.rotation = canvasAnchor.transform.rotation;
	}

	public static void WaveBreak(bool b) {
		waveBreakActive = b;
	}

	public static void GameOver(bool b) {
		gameOverActive = b;
	}

	public static void SetScore(int s) {
		score = s;
	}

	public static void SetHealth(int h) {
		health = h;
	}

	public void StartGameButton()
	{
		this.SetHUD (true);
		this.mainMenuPanel.SetActive (false);
		Utilities.GetSpawner ().PauseMenu (false);
		Soundtrack.mainThemeVolume = inGameVolume;
	}

	public void QuitDesktopButton () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void ResumeButton() {
		this.SetHUD (true);
		Utilities.GetSpawner ().PauseMenu (false);
		this.pauseMenuPanel.SetActive (false);
		if (Soundtrack.IsMainActive ()) {
			Soundtrack.mainThemeVolume = inGameVolume;
		} else {
			Soundtrack.darkThemeVolume = inGameVolume;
		}
	}

	public void RestartButton () {
		Game game = GameObject.FindObjectOfType<Game>();

		if (this.gameOverUI.activeSelf) {
			Utilities.GetSpawner ().PauseMenu (true);
			TempUIManager.GameOver (false);
		}

		game.ResetGame ();
		this.pauseMenuPanel.SetActive (false);
		this.gameOverUI.SetActive (false);
		this.waveBreakUI.SetActive (false);
		this.SetHUD (false);
		this.mainMenuPanel.SetActive (true);

		if (Soundtrack.IsMainActive ()) {
			Soundtrack.mainThemeVolume = menuVolume;
		} else {
			Soundtrack.SwitchTracks ();
			Soundtrack.mainThemeVolume = menuVolume;
		}
	}

	public void HighScoresButton () {
		this.mainMenuPanel.SetActive (false);
		this.highScorePanel.SetActive (true);
	}

	public void HighScoresBackButton () {
		this.mainMenuPanel.SetActive (true);
		this.highScorePanel.SetActive (false);
	}

	public void SwitchToPauseMenuButton () {
		this.pauseMenuPanel.SetActive(true);
	}

	public void TestDebug() {
		Debug.Log ("Pressed button: " + gameObject.name);
	}

	private void SetHUD(bool b) {
		healthSlider.gameObject.SetActive (b);
		scoreText.gameObject.SetActive (b);
	}
}