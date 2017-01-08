﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour {
	private StatePatternAI parent;

	void Start() {
		parent = this.transform.GetComponentInParent<StatePatternAI>();
		Debug.Log("1236+");
	}
	void OnCollisionEnter(Collision collision){
		Debug.Log("hit!");
		parent.OnCollisionBody(collision);
	}

	// void OnTriggerEnter(Collider collision){
	// 	Debug.Log("hit!");
	// 	// parent.OnCollisionBody(collision);
	// }

}