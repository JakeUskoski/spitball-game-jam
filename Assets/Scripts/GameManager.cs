using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private float m_TimeLimit = 300f;

	private GameObject player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
