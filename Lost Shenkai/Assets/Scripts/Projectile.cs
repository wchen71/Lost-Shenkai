using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
	public float maxRotSpeed;
	public int damage;
	public bool deflectable;

	public GvrAudioSource deflectNoise;
	public GvrAudioSource hitNoisePrefab;

    private Vector3 playerDir;
	private Player player;

    private void Awake()
	{
		player = Utilities.GetPlayerHead ();
		playerDir = (player.transform.position - gameObject.transform.position).normalized;
	}

	void Start() {
		this.gameObject.GetComponent<Rigidbody> ().AddForce (playerDir * speed);

		this.gameObject.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (
			Random.Range (-maxRotSpeed, maxRotSpeed),
			Random.Range (-maxRotSpeed, maxRotSpeed),
			Random.Range (-maxRotSpeed, maxRotSpeed));
	}

	void OnCollisionEnter(Collision col) {
		if (Utilities.IsPlayerHead (col.collider)) {
			this.Attack ();
		} else if (Utilities.IsPlayerHand (col.collider)) {
			HandManager handMan = col.gameObject.GetComponent<HandManager> ();
			if (handMan) {
				handMan.SpawnHitParticles ();
				handMan.VibrateController (0.3f, 1f);
			}

			if (deflectable) {
				this.gameObject.GetComponent<Rigidbody> ().AddForce (col.contacts [0].normal * speed);
				deflectNoise.Play ();
				gameObject.GetComponent<GvrAudioSource> ().Stop ();
			} else {
				this.Attack ();
			}
		}
	}

	private void Attack() {
		Player.singlePlayer.SubtractHealth (damage);
		Instantiate (hitNoisePrefab, gameObject.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Limiter") {
			Destroy (gameObject);
			Game.AddScore (1);
		}
	}
}
