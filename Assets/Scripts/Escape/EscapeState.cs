﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EscapeState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	public Transform wayPoint;
	public float speed;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public EscapeState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		name = "EscapeState";
		wayPoint = new GameObject ().transform;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.escapeState;
		wayPoint.position = new Vector3 (AI.player.transform.position.x + Random.Range (-10f, 10f),
			//			this.transform.position.y + Random.Range (-10f, 100f),
			AI.player.transform.position.y + Random.Range (0f, 2f),
			// Random.Range (0f, 10f),
			AI.transform.position.z + Random.Range(-6f, -2f));
		// AI.transform.GetComponent<Rigidbody> ().isKinematic = true;
		AI.speed = 10f;
		if(AI.isRage){
			AI.speed = 15f;
		}
	}

	public void UpdateState(){
		Escape ();
	}

	public void EndState(){
		// AI.transform.GetComponent<Rigidbody> ().isKinematic = false;
		AI.NextState();
	}

	public void StateChangeCondition(){

		EndState ();
		// AI.seekState.StartState ();
	}

	public void Escape(){
		float step = speed * Time.deltaTime;
		AI.transform.position = Vector3.MoveTowards(AI.transform.position, wayPoint.position, step);
		if(Vector3.Distance(AI.transform.position, wayPoint.position) < 0.1f){
			//It is within ~0.1f range, do stuff
//			wayPoint.position = new Vector3 (AI.player.transform.position.x + Random.Range (-10f, 10f),
//				AI.player.transform.position.y + Random.Range (0f, 10f),
//				// Random.Range (0f, 10f),
//				AI.player.transform.position.z + Random.Range (-10f, 10f));
			StateChangeCondition ();

		}
		AI.transform.LookAt(AI.player.transform);
	}
}
