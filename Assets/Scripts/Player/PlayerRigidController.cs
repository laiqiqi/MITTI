using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidController : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "AISword"){
			Debug.Log("hit");
			col.rigidbody.AddForce(col.impulse * (100 ));

		}
	}
}
