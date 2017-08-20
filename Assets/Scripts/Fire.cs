using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	[SerializeField] private float m_Duration = 3f;
	[SerializeField] private float m_TravelDistance = 3f;
	[SerializeField] private Vector3 m_StartScale = new Vector3 (0.1f, 0.1f, 1f);
	[SerializeField] private Vector3 m_EndScale = new Vector3(3, 3, 1);

	private float currentTime;
	private Vector3 startTravel;
	private Vector3 endTravel;

	// Use this for initialization
	void Start () {
		currentTime = 0f;
		startTravel = transform.position;
		endTravel = (transform.right * m_TravelDistance) + transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime > m_Duration) {
			currentTime = m_Duration;
			Destroy (gameObject);
		}

		transform.position = Vector3.LerpUnclamped (startTravel, endTravel, currentTime);
		transform.localScale = Vector3.LerpUnclamped (m_StartScale, m_EndScale, currentTime);
	}

	private void OnCollisionEnter2D(Collision2D coll) {
		string tag = coll.gameObject.tag;

		if (tag != "Enemy" && tag != "Player") {
			if (tag != "EnemyBlock") {
				Debug.Log (tag);
				if (tag == "Spitball" || coll.gameObject.transform.parent != null && coll.gameObject.transform.parent.tag == "Spitball") {
					Spitball spitball = coll.gameObject.transform.parent.GetComponent<Spitball> ();
					if (spitball != null) {
						spitball.HurtDuration ();
					}
				}
				Destroy (gameObject);
			}
		} else {
			coll.gameObject.GetComponent<Combat> ().Damage ();
			Destroy (gameObject);
		}
	}
}
