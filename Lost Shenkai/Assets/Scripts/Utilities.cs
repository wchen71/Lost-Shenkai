using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

	private static Player playerHead;
	private static ProjectileSpawner spawner;

	public static bool IsPlayerHead (Collider col) {
		return col.tag == "PlayerHead";
	}

	public static bool IsPlayerHand (Collider col) {
		return col.tag == "PlayerHand";
	}

	public static ProjectileSpawner GetSpawner() {
		if (spawner == null) {
			spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<ProjectileSpawner> ();
		}
		return spawner;
	}

	public static Player GetPlayerHead() {
		if (playerHead == null) {
			playerHead = GameObject.FindGameObjectWithTag ("PlayerHead").GetComponent<Player> ();
		}
		return playerHead;
	}
}