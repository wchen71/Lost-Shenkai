using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

	public float timeToDestroy;

	private float timer;

	void Start() {
		timer = timeToDestroy;
	}

	void Update() {
		if (timer <= 0f) {
			Destroy (gameObject);
		}
		else {
			timer -= Time.deltaTime;
		}
	}
}
