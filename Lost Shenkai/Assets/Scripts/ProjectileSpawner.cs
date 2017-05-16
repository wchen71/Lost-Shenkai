using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileSpawner : MonoBehaviour {

	public float spawnRadius;
	public float spawnHeightMax;
	public float spawnHeightMin;
	public float spawnDelay;
	public int amountPerSpawn;
	public float waveDelay;

	public Projectile[] projectilePrefabs;
	public Projectile bossProjectilePrefab;

	public UnityEvent setLightCallback;

	private List<Projectile> projectileInstances;
	private float timer;
	private bool waveBreak;
	private bool pauseMenu;
	private bool bossSpawned;

	void Awake() {
		timer = spawnDelay;
		pauseMenu = true;
	}

	// Use this for initialization
	void Start () {
		waveBreak = false;
		bossSpawned = false;
		this.projectileInstances = new List<Projectile> ();
	}

	public void PauseMenu(bool b) {
		if (b) {
			pauseMenu = true;
			CleanupProjectiles ();
			for (int i = 0; i < projectileInstances.Count; i += 1) {
				Destroy (projectileInstances [i].gameObject);
			}
			projectileInstances.Clear ();
		} else {
			pauseMenu = false;
		}
	}

	public void PauseWaveBreak() {
		waveBreak = true;
	}

	private void CleanupProjectiles() {
		for (int i = projectileInstances.Count - 1; i >= 0; i -= 1) {
			if (projectileInstances [i] == null) {
				projectileInstances.RemoveAt (i);
			}
		}
	}

	public void PauseProjectilesGameOver() {
		for (int i = 0; i < projectileInstances.Count; i += 1) {
			if (projectileInstances [i] != null) {
				projectileInstances [i].GetComponent<Rigidbody> ().isKinematic = true;
				projectileInstances [i].GetComponent<Rigidbody> ().velocity = new Vector3(0f, 0f, 0f);
			}
		}
		pauseMenu = true;
	}

	public void SpawnBoss() {
		this.SpawnProjectileAtRandomPoint (this.bossProjectilePrefab);
	}
	
	// Update is called once per frame
	void Update () {
		if (!pauseMenu) {
			if (timer <= 0f) {
				if (waveBreak) {
					if (projectileInstances.Count < 1) {
						Debug.Log ("Between Waves");
						TempUIManager.WaveBreak (true);
						waveBreak = !waveBreak;
						timer = waveDelay;
						bossSpawned = false;
						Player.singlePlayer.SetHealthToMax ();
						setLightCallback.Invoke ();
					} else {
						CleanupProjectiles ();
					}
				} else {
					if (!bossSpawned) {
						SpawnBoss ();
						bossSpawned = true;
					}
					TempUIManager.WaveBreak (false);
					for (int i = 0; i < amountPerSpawn; i += 1) {
						Spawn ();
					}
					timer = spawnDelay;
				}
			} else {
				timer -= Time.deltaTime;
			}
		}
	}

	private void Spawn() {
		this.SpawnProjectileAtRandomPoint (this.projectilePrefabs [Random.Range (0, this.projectilePrefabs.Length)]);
	}

	private void SpawnProjectileAtRandomPoint(Projectile p) {
		float ang = Random.value * 360;
		float x = transform.position.x + (spawnRadius * Mathf.Cos(ang * Mathf.Deg2Rad));
		float z = transform.position.z + (spawnRadius * Mathf.Sin(ang * Mathf.Deg2Rad));
		float y = Random.Range (spawnHeightMin, spawnHeightMax);
		Vector3 pos = new Vector3 (x, y, z);
		Projectile projectile = Instantiate (p, pos, Quaternion.identity);
		projectileInstances.Add (projectile);
	}
}