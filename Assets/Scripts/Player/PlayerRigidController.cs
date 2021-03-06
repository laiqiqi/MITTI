using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerRigidController : MonoBehaviour {
	// Use this for initialization
	public GameObject metalHitFX;

	void Start () {
		Physics.IgnoreCollision( GetComponent<BoxCollider>(), Player.instance.headCollider );
		Physics.IgnoreCollision( GetComponent<BoxCollider>(), Player.instance.bodyCollider );
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "AISword") {
			Debug.Log ("hit");
			col.rigidbody.AddForce (col.impulse * (100));

		} else if (col.gameObject.tag == "AISwordShoot"){
			col.rigidbody.AddForce (col.impulse * (10));
		}
		// Debug.Log ("contactsPoint         "+col.contacts[0].point);
		// Debug.Log ("position              "+this.transform.position);
		Instantiate (metalHitFX, col.contacts[0].point, Quaternion.LookRotation(col.transform.position));
	}
}
