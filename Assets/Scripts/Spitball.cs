using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitball : MonoBehaviour {

	[SerializeField] private float m_FlyInterpolation = 0.5f;
	[SerializeField] private float m_StickDuration = 3f;
	[SerializeField] private Vector3 startScale = new Vector3 (10, 10, 1);
	[SerializeField] private Vector3 endScale = new Vector3 (1, 1, 1);

	private Vector3 endPosition;
	private bool moving;
	private BoxCollider2D[] colliders;

	private void Awake () {
		transform.localScale = startScale;
		colliders = gameObject.GetComponentsInChildren<BoxCollider2D> ();
		foreach(BoxCollider2D collider in colliders) {
			collider.enabled = false;
		}
	}

	// Use this for initialization
	private void Start () {
		moving = true;
		endPosition = new Vector3 (transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	private void Update () {
		
		if (moving) {
			Vector3 currentPosition = transform.position;

			currentPosition = Vector3.Lerp (currentPosition, endPosition, m_FlyInterpolation);
			transform.localScale = Vector3.Lerp (transform.localScale, endScale, m_FlyInterpolation);

			if (currentPosition.z >= -0.1) {
				currentPosition.z = 0;
				transform.localScale = endScale;
				moving = false;

				foreach(BoxCollider2D collider in colliders) {
					collider.enabled = true;
				}

				bool flag = false;
				Collider2D[] otherColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
				for (int i = 0; i < otherColliders.Length; i++)
				{
					if (otherColliders [i].gameObject.tag == "Enemy") {
						otherColliders [i].gameObject.GetComponent<Combat> ().Damage ();
						flag = true;
					}
				}

				if (flag) {
					Destroy (gameObject);
				}
			}

			transform.position = currentPosition;
		} else {
			m_StickDuration -= Time.deltaTime;

			if (m_StickDuration <= 0) {
				Destroy (gameObject);
			}
		}
	}

	public void HurtDuration() {
		Debug.Log ("hello");
		m_StickDuration -= 0.5f;
	}
}
