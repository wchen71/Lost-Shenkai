using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static GamePlatform platform = GamePlatform.VR_Oculus;
	public int numWaveCycles;

	private static int score;
	private static int highScore;

	private int nextWave;
	private int waveMultiplier;

	public float lightLoss;
	public float fogLoss;

	private bool gettingDarker;
	private int curWaveInCycle;

	public float lightSwitchSpeed;
	private Light mainLight;
	private float lightAmount;
	private float origLightIntensity;
	private float origFogIntensity;
	private float origSkyboxExposure;

	private int origAmountPerSpawn;
	private float origSpawnDelay;

	void Awake () {
		highScore = GetHighScore ();
		nextWave = 10;
		waveMultiplier = 2;

		mainLight = GameObject.FindGameObjectWithTag ("MainLight").GetComponent<Light> ();
	}

	// Use this for initialization
	void Start () {
		this.gettingDarker = true;
		this.curWaveInCycle = this.numWaveCycles;
		this.lightAmount = (float)this.curWaveInCycle / (float)this.numWaveCycles;
		this.origLightIntensity = this.mainLight.intensity;
		this.origFogIntensity = RenderSettings.fogDensity;
		GameSkybox.current.SetExposure (GameSkybox.skyboxExposureDefault);
		this.origSkyboxExposure = GameSkybox.current.GetExposure ();

		ProjectileSpawner ps = Utilities.GetSpawner ();
		ps.setLightCallback.AddListener (SetLight);
		this.origAmountPerSpawn = ps.amountPerSpawn;
		this.origSpawnDelay = ps.spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		WaveSet ();

		this.mainLight.intensity = Mathf.Lerp (this.mainLight.intensity, this.origLightIntensity * this.lightAmount, lightSwitchSpeed);
		RenderSettings.fogDensity = Mathf.Lerp (RenderSettings.fogDensity, this.origFogIntensity * this.lightAmount, lightSwitchSpeed);
		GameSkybox.current.SetExposure (Mathf.Lerp (GameSkybox.current.GetExposure (), this.origSkyboxExposure * this.lightAmount, lightSwitchSpeed));
	}

	public static int GetHighScore () {
		return PlayerPrefs.GetInt ("HighScore", 0);
	}

	private static void CheckHighScore () {
		if (score > highScore) {
			WriteHighScore ();
		}
	}

	private static void WriteHighScore () {
		PlayerPrefs.SetInt ("HighScore", score);
	}

	public static void OnGameOver () {
		Utilities.GetSpawner ().PauseProjectilesGameOver ();
		TempUIManager.GameOver (true);
		CheckHighScore();
	}

	public static void AddScore (int amount) {
		score += amount;
		TempUIManager.SetScore (score);
	}

	public static void SetScore (int newScore) {
		score = newScore;
		TempUIManager.SetScore (score);
	}

	private void WaveSet() {
		if (score >= nextWave) {
			nextWave *= waveMultiplier;
			ProjectileSpawner ps = Utilities.GetSpawner ();
			ps.PauseWaveBreak ();
			if (score >= 160) {
				Debug.Log ("You're too good stop.");
				ps.amountPerSpawn = 10;
				ps.spawnDelay = 0.5f;
			} else if (score >= 80) {
				ps.amountPerSpawn = 3;
			} else if (score >= 40) {
				ps.amountPerSpawn = 1;
			} else if (score >= 20) {
				ps.amountPerSpawn = 2;
			} else if (score >= 10) {
				ps.amountPerSpawn = 1;
			}
		}
	}

	private void SetLight() {
		if (this.gettingDarker) {
			this.curWaveInCycle--;
			if (this.curWaveInCycle <= 0) {
				this.gettingDarker = !this.gettingDarker;
			}
		} else {
			this.curWaveInCycle++;
			if (this.curWaveInCycle >= this.numWaveCycles) {
				this.gettingDarker = !this.gettingDarker;
			}
		}
		this.lightAmount = (float)this.curWaveInCycle / (float)this.numWaveCycles;

		if (curWaveInCycle <= 1) {
			if (Soundtrack.IsMainActive ()) {
				Soundtrack.SwitchTracks ();
			}
		} else {
			if (!Soundtrack.IsMainActive ()) {
				Soundtrack.SwitchTracks ();
			}
		}
	}

	public void ResetGame() {
		highScore = GetHighScore ();
		nextWave = 10;
		waveMultiplier = 2;

		this.gettingDarker = true;
		this.curWaveInCycle = this.numWaveCycles;
		this.lightAmount = (float)this.curWaveInCycle / (float)this.numWaveCycles;
		this.mainLight.intensity = this.origLightIntensity;
		RenderSettings.fogDensity = this.origFogIntensity;
		GameSkybox.current.SetExposure (GameSkybox.skyboxExposureDefault);

		ProjectileSpawner ps = Utilities.GetSpawner ();
		ps.amountPerSpawn = this.origAmountPerSpawn;
		ps.spawnDelay = this.origSpawnDelay;

		Player.singlePlayer.SetHealthToMax ();
		Game.SetScore (0);
	}
}