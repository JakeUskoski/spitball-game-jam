using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour {

	[SerializeField] private GameObject m_FirePrefab;
	[SerializeField] private float m_FireRate = 0.5f;
	[SerializeField] private float m_ShootAngle = -180f;
	[SerializeField] private bool m_Spread;
	[SerializeField] private float m_SpreadRange = 15f;
	[SerializeField] private float m_SpreadIncrement = 0.1f;

	private float currentAngle;
	private bool shoot;
	private float shootCooldown;
	private float direction;

	// Use this for initialization
	private void Start () {
		currentAngle = m_ShootAngle;
		shootCooldown = 0f;
		shoot = true;
		direction = 1f;
	}


	private void Update () {
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
			if (shootCooldown < 0) {
				shootCooldown = 0;
				shoot = true;
			}
		}
	}

	// Update is called once per frame
	private void FixedUpdate () {
		if (shoot) {
			shoot = false;
			shootCooldown = m_FireRate;
			if (m_Spread) {
				currentAngle += m_SpreadRange * m_SpreadIncrement * direction;
				if (currentAngle == m_ShootAngle + m_SpreadRange * direction) {
					direction *= -1;
				}
			}

			Instantiate (m_FirePrefab, transform.position, Quaternion.Euler (new Vector3(0f, 0f, currentAngle)));
		}
	}
}
