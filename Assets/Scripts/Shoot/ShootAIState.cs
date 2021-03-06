﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootAIState : AIState {
	private readonly StatePatternAI enemy;
	public string name{ get;}
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public ShootAIState(StatePatternAI statePatternAI){
		enemy = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
//		enemy.currentState = enemy.shootState;
	}

	public void UpdateState(){
		GameObject bulletIn = GameObject.Instantiate (enemy.bullet.gameObject, enemy.transform.position + new Vector3(0f, 0f, 0f), enemy.transform.rotation);
		Physics.IgnoreCollision(bulletIn.GetComponent<Collider>(), enemy.transform.GetChild(0).GetComponent<Collider>());
		Physics.IgnoreCollision(bulletIn.GetComponent<Collider>(), enemy.transform.GetChild(1).GetComponent<Collider>());
		Physics.IgnoreCollision(bulletIn.GetComponent<Collider>(), enemy.GetComponent<Collider>());
//		Debug.Log("aaaaaaaaaaaaaa"+enemy.GetComponentsInChildren<Transform> ()[0].name);
//		Debug.Log("aaaaaaa"+enemy.transform.GetChild(0).name);
		StateChangeCondition ();
	}

	public void EndState(){
		
	}

	public void StateChangeCondition(){
		EndState ();
		enemy.floatingState.StartState ();
	}
}
