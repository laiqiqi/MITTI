using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDirectionController : MonoBehaviour {
	public GameObject AI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
//		Vector3 swordDirection = AI.GetComponent<StatePatternAI> ().swordDirection;
//		swordDirection = -swordDirection;
		AI.GetComponent<StatePatternAI> ().swordDirection = -AI.GetComponent<StatePatternAI> ().swordDirection;
	}
}
