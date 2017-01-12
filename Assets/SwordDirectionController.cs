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

	void OnTriggerEnter(Collider col) {
//		Vector3 swordDirection = AI.GetComponent<StatePatternAI> ().swordDirection;
//		swordDirection = -swordDirection;

		//using
//		AI.GetComponent<StatePatternAI> ().swordDirection = -AI.GetComponent<StatePatternAI> ().swordDirection;
//		Debug.Log (AI.GetComponent<StatePatternAI> ().currentState.name);
		if (AI.GetComponent<StatePatternAI> ().currentState.name != "parryAIState") {
			AI.GetComponent<StatePatternAI> ().parryState.StartState ();
		}
	}

}
