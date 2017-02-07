using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerRigidController : MonoBehaviour {
	// private Vector3 oldposition;
	private float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Vector3 vel = this.GetComponent<Rigidbody>().velocity;      //to get a Vector3 representation of the velocity
		// speed = vel.magnitude;
		// Debug.Log(speed);
		// Debug.Log();
		// Debug.Log(this.transform.root.GetComponentInChildren<VRTK_ControllerEvents>().GetVelocity().magnitude);
	}

	void OnCollisionEnter(Collision col){
		// Vector3 dir = col.transform.position - this.transform.position;
		// Debug.Log("speed    :"  +speed);
		// VRTK_SDK_Bridge.IsControllerLeftHand(this.gameObject);
		// Debug.Log(col.transform.tag);
		if(col.transform.tag == "AI"){
			// Debug.Log(this.transform.root.GetComponentInChildren<VRTK_ControllerEvents>().GetVelocity().magnitude);
			// if(this.transform.root.GetComponentInChildren<VRTK_ControllerEvents>().GetVelocity().magnitude > 0.1f){
			foreach(ContactPoint contact in col.contacts) {
						
					//  contacts = collision.contacts.Length;
			
					//  if(Input.GetKey(KeyCode.W)){
					
					Vector3 dir;
					dir = (contact.point - transform.position);
					// col.rigidbody.AddForceAtPosition(Vector3.Cross(transform.right, dir).normalized * (2000 / col.contacts.Length), contact.point);
						
					col.rigidbody.AddForceAtPosition(-col.impulse.normalized * 2500f , contact.point);
					// col.rigidbody.AddForceAtPosition(-col.impulse.normalized * (this.transform.root.GetComponentInChildren<VRTK_ControllerEvents>().GetVelocity().magnitude * 1000f ), contact.point);
						
					//  }
					
				}
			// }
		}
		// 		if(this.transform.root.GetComponentInChildren<VRTK_ControllerEvents>().GetVelocity().magnitude > 1f){
		// 	// Vector3 dir = col.transform.position - this.transform.position;
		// 	// Vector3 dir = col.contacts[0].point - this.GetComponent<Co;

		// 	col.rigidbody.AddForce (this.transform.forward * 3000);
		// 	if(col.transform.tag == "AI")
		// 		col.gameObject.GetComponent<StatePatternAI>().isParry = true;
		// 	Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaa"+"CollisionEnter");
		// }
//		col.rigidbody.AddForce (dir * 300);
	}
}
