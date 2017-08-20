using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	[SerializeField] private int m_MaxHealth = 3;
	[SerializeField] private float m_InvincibilityDuration = 2f;

	private int currentHealth;
	private float currentInvincibility;

	// Use this for initialization
	private void Start () {
		currentHealth = m_MaxHealth;
		currentInvincibility = 0;
	}

	private void Update() {
		if (currentInvincibility > 0) {
			currentInvincibility -= Time.deltaTime;
			if (currentInvincibility < 0) {
				currentInvincibility = 0;
			}
		}
	}

	public void Damage() {
		if (currentInvincibility == 0 && currentHealth > 0) {
			currentHealth--;
			currentInvincibility = m_InvincibilityDuration;
		}
	}

	public void Heal() {
		if (currentHealth < m_MaxHealth) {
			currentHealth++;
		}
	}

	public int getHealth() {
		return currentHealth;
	}
}
