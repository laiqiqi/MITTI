using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFloatingSword : MonoBehaviour {
	private float speed;
	public int state;
	public bool isHit;
	public bool isHitOther;
	private float timeCount;
	public bool virtualSword;
	private bool isHide;
	// Use this for initialization
	void Start () {
		speed = 300;
		isHit = false;
//		isHide = false;
		timeCount = 0;
//		virtualSword = false;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("Velocity    "+this.GetComponent<Rigidbody> ().velocity);
//		Debug.Log("Angular     "+this.GetComponent<Rigidbody> ().angularVelocity);
		if (state == 1) {
			this.transform.Rotate (0f, 0f, speed * Time.deltaTime);
		} else if (state == 2) {
			
		} else if (state == 0) {
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = this.GetComponent<Rigidbody> ().useGravity;
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().isKinematic = this.GetComponent<Rigidbody> ().isKinematic;
			if (this.transform.position.y < 4f && this.transform.position.y > 1f && !isHitOther) {
				RaycastHit hit;
				if (Physics.Raycast (this.transform.position, this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				} else if (Physics.Raycast (this.transform.position, -this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				} else if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 3.1f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer ("AISword");
						this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
					}
				}
			}
		} else if (state == 3) {
			//While floating in air by shooting
			this.GetComponent<Rigidbody> ().AddForce (-this.transform.forward*200f);
//			RaycastHit hit;
//			if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 3.1f)) {
//				print ("Ray    " + hit.transform.tag);
//				if (hit.transform.tag == "Ground") {
//			this.gameObject.layer = LayerMask.NameToLayer ("AISword");
//			this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");

		} else if (state == 4) {
			//Pierce through the ground
//			this.GetComponent<Rigidbody> ().isKinematic = true;
//			this.transform.position += -this.transform.forward*Time.deltaTime;
//			this.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer ("AISword");
		}

		if(virtualSword && isHide){
			if (timeCount >= 5) {
				this.gameObject.SetActive (false);
			}
			timeCount += Time.deltaTime;
		}
	}

	void OnCollisionStay(Collision other){
		if (other.transform.tag == "Player") {
			isHit = true;
		} else if(other.transform.tag == "SwordGround"){
			if (state == 0 || state == 4) {
				this.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}else{
			Debug.Log ("hit other");
			isHitOther = true;
			if (state == 3) {
				isHide = true;
				if (other.transform.tag == "Ground") {
					RaycastHit hit;
					if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 5f)) {
						if (hit.transform.tag == "Ground") {
							Debug.Log ("Sword 44444");
							state = 4;
							this.transform.position += -this.transform.forward * 2;
							this.GetComponent<Rigidbody> ().isKinematic = true;
						}else {
							this.GetComponent<Rigidbody> ().useGravity = true;
							state = 0;
						}
					}
				} else {
					this.GetComponent<Rigidbody> ().useGravity = true;
					state = 0;
				}
			}
		}
	}

	void OnCollisionExit(Collision other){
		if (other.transform.tag == "Player") {
			isHit = false;
		}else {
			isHitOther = false;
		}
	}
}
