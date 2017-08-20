using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

	private GameManager manager;

	void Awake() {
		manager = Camera.main.GetComponent<GameManager> ();
	}
		
	void OnTiggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			manager.WinGame ();
		}
	}
}
