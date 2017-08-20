using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyInput : MonoBehaviour {

	[SerializeField] private float m_Direction = -1f;
	[SerializeField] private bool m_Bouncing = false;
	[SerializeField] private float m_BounceDelay = 0.2f;
	[SerializeField] private bool m_DetectEdges = false;

	private GenericController m_Character;
	private Combat combat;
	private float flipCooldown = 0f;
	private float bounceDelay = 0f;
	private bool bounce;


	private void Awake() {
		m_Character = GetComponent<GenericController>();
		combat = GetComponent<Combat>();
	}

	private void Start() {
		bounce = m_Bouncing;
	}

	private void Update() {
		updateInit ();
		if (combat.getHealth () == 0) {
			Destroy (gameObject);
		}
	}

	public void updateInit() {
		if (flipCooldown > 0) {
			flipCooldown -= Time.deltaTime;
			if (flipCooldown < 0) {
				flipCooldown = 0;
			}
		}
	}	

	private void FixedUpdate() {
		FixedUpdateInit ();
	}

	public void FixedUpdateInit() {
		if (m_Character.getGrounded () && bounceDelay > 0) {
			bounceDelay -= Time.deltaTime;
			if (bounceDelay <= 0) {
				bounceDelay = 0;
				if (m_Bouncing) {
					bounce = true;
				}
			}
		}

		m_Character.Move(m_Direction, bounce);
		if (bounce) {
			bounceDelay = m_BounceDelay;
			bounce = false;
		}
	}

	private void OnCollisionEnter2D (Collision2D coll) {
		OnCollisionEnter2DInit (coll);
	}

	public void OnCollisionEnter2DInit(Collision2D coll) {
		if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Untagged") {
			m_Direction *= -1;
		} else if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<Combat> ().Damage ();
		}
	}

	public void Flip() {
		if (flipCooldown == 0) {
			m_Direction *= -1;
			flipCooldown = 0.2f;
		}
	}

	private void OnTriggerEnter2D(Collider2D coll) {
		if (m_DetectEdges && coll.gameObject.tag == "EdgeGuard") {
			Flip ();
		}
	}
}
