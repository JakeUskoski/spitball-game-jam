using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField] private string m_NextSceneName;
	[SerializeField] private float m_TimeLimit = 300f;

	private GameObject player;
	private Combat playerCombat;
	private bool isPlaying;

	private Text winText;
	private Text loseText;
	private Text timerText;

	private float preEndTimer;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerCombat = player.GetComponent<Combat> ();
		winText = GameObject.FindGameObjectWithTag ("WinText").GetComponent<Text> ();
		loseText = GameObject.FindGameObjectWithTag ("LoseText").GetComponent<Text> ();
		timerText = GameObject.FindGameObjectWithTag ("TimerText").GetComponent<Text> ();

		isPlaying = true;
		winText.enabled = false;
		loseText.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (isPlaying) {
			//Update timer
			timerText.text = Mathf.RoundToInt (m_TimeLimit).ToString ();

			if (player == null) {
				isPlaying = false;
				loseText.enabled = true;
				preEndTimer = 3f;
			}

			m_TimeLimit -= Time.deltaTime;
			if (m_TimeLimit <= 0) {
				m_TimeLimit = 0;
				loseText.enabled = true;
				isPlaying = false;
				preEndTimer = 3f;
			}
		} else {
			preEndTimer -= Time.deltaTime;

			if (preEndTimer <= 0) {
				if (loseText.enabled) {
					SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				} else {
					SceneManager.LoadScene (m_NextSceneName);
				}
			}
		}
	}

	public void WinGame() {
		if (isPlaying) {
			isPlaying = false;
			winText.enabled = true;
			preEndTimer = 3f;
		}
	}
}
