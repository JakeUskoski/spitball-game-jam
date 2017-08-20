using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallTrigger : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10) {
			transform.position = new Vector3 (0, 0, 0); //change these values to choose where character respawns at
		}
	}
}
