using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyInput : MonoBehaviour {

	[SerializeField] private float m_Direction = -1f;
	[SerializeField] private bool m_Bouncing = false;
	[SerializeField] private bool m_DetectEdges = false;

	private GenericController m_Character;
	private float flipCooldown = 0f;


	private void Awake() {
		m_Character = GetComponent<GenericController>();
	}

	private void Update() {
		updateInit ();
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
		m_Character.Move(m_Direction, m_Bouncing);
	}

	private void OnCollisionEnter2D (Collision2D coll) {
		OnCollisionEnter2DInit (coll);
	}

	public void OnCollisionEnter2DInit(Collision2D coll) {
		if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Enemy") {
			m_Direction *= -1;
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
