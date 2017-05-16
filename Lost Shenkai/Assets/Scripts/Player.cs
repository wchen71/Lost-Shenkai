using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player singlePlayer;

	public const int MAX_HEALTH = 100;
	private int health;
	public float regenDelay;
	public int regenFactorPerSec;

	private float lastTimeHit;
	private float regenFactorTimer;

	void Awake () {
		// Singleton for single player
		if (singlePlayer == null) {
			singlePlayer = this;
		}

		health = MAX_HEALTH;
	}

	// Use this for initialization
	void Start () {
		lastTimeHit = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (health < MAX_HEALTH && Time.time - this.lastTimeHit >= regenDelay) {
			if (regenFactorTimer <= 0f) {
				this.AddHealth (1);
				regenFactorTimer = regenFactorPerSec;
			} else {
				regenFactorTimer -= Time.deltaTime;
			}
		}
	}

	public void AddHealth (int amount) {
		if (this.health + amount <= MAX_HEALTH) {
			this.health += amount;
		} else {
			this.health = MAX_HEALTH;
		}
		TempUIManager.SetHealth (this.health);
	}

	public void SetHealthToMax() {
		this.health = MAX_HEALTH;
		TempUIManager.SetHealth (this.health);
	}

	public void SubtractHealth (int amount) {
		this.health -= amount;
		TempUIManager.SetHealth (this.health);
		this.CheckDead ();

		this.lastTimeHit = Time.time;
	}

	private void CheckDead () {
		if (this.health <= 0) {
			KillPlayer ();
		}
	}

	private void KillPlayer () {
		GameSkybox.current.SetExposure (GameSkybox.skyboxExposureDefault);
		Game.OnGameOver ();
	}
}