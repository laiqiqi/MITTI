using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		Vector3 dir = col.transform.position - this.transform.position;
		col.rigidbody.AddForce (dir * 300);
	}
}
