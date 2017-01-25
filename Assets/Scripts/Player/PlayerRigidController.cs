using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidController : MonoBehaviour {
	// private Vector3 oldposition;
	private float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = this.GetComponent<Rigidbody>().velocity;      //to get a Vector3 representation of the velocity
		speed = vel.magnitude;
		Debug.Log(speed);
	}

	void OnCollisionEnter(Collision col){
//		Vector3 dir = col.transform.position - this.transform.position;
		Debug.Log("speed    :"  +speed);
		// if(speed> 0.5f){
			Vector3 dir = col.transform.position - this.transform.position;
			col.rigidbody.AddForce (this.transform.forward * 3000);
			if(col.transform.tag == "AI")
				col.gameObject.GetComponent<StatePatternAI>().isParry = true;
			Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaa"+"CollisionEnter");
		// }
//		col.rigidbody.AddForce (dir * 300);
	}
}
