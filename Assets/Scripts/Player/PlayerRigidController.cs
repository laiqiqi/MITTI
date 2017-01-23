using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidController : MonoBehaviour {
	private Vector3 oldposition;
	private float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = this.GetComponent<CharacterController> ().velocity;      //to get a Vector3 representation of the velocity
		speed = vel.magnitude;
//		Debug.Log(speed);
	}

	void OnCollisionEnter(Collision col){
//		Vector3 dir = col.transform.position - this.transform.position;
		if(speed> 3f ){
			Vector3 dir = col.transform.position - this.transform.position;
			col.rigidbody.AddForce (dir * 1000);
			Debug.Log ("aaaaaaaaaaaaaaaaaaaaaaaa"+"CollisionEnter");
		}
//		col.rigidbody.AddForce (dir * 300);
	}
}
