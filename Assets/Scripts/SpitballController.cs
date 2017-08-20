using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitballController : MonoBehaviour {

	[SerializeField] private float m_MaxSpit = 3f;
	[SerializeField] private float m_SpitCost = 1f;
	[SerializeField] private float m_SpitRechargeRatePerSecond = 1f;
	[SerializeField] private GameObject m_SpitballPrefab;
	[SerializeField] private float m_SpitCooldown = 0.1f;

	private float currentSpit;
	private float spitTimer;
	private bool canSpit;
	private bool spit;
	private Vector3 spitPosition;

	// Use this for initialization
	private void Start () {
		currentSpit = m_MaxSpit;
		spitTimer = 0;
		spit = false;
		canSpit = true;
	}
	
	// Update is called once per frame
	private void Update () {
		if (currentSpit < m_MaxSpit) {
			currentSpit += m_SpitRechargeRatePerSecond * Time.deltaTime;
			if (currentSpit > m_MaxSpit) {
				currentSpit = m_MaxSpit;
			}
		}

		if (Input.GetMouseButtonDown (0) && canSpit && currentSpit >= m_SpitCost) {
			spit = true;
			spitPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			canSpit = false;
		}
	}

	private void FixedUpdate() {
		if (spit) {
			SpawnSpit (spitPosition);
			currentSpit -= m_SpitCost;
			spitTimer = m_SpitCooldown;
			spit = false;
		} else if (spitTimer > 0) {
			spitTimer -= Time.deltaTime;
			if (spitTimer < 0) {
				spitTimer = 0;
			}

			if (spitTimer == 0) {
				canSpit = true;
			}
		}
	}

	private void SpawnSpit(Vector3 position) {
		Instantiate(m_SpitballPrefab, new Vector3(position.x, position.y, -10), Quaternion.identity);
	}
}
