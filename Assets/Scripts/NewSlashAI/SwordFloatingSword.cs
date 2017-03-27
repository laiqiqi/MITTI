using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFloatingSword : MonoBehaviour {
	private float speed;
	public int state;
	public bool isHit;
	public bool isHitOther;
	private float timeCount;
	// Use this for initialization
	void Start () {
		speed = 300;
		isHit = false;
		timeCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("Velocity    "+this.GetComponent<Rigidbody> ().velocity);
//		Debug.Log("Angular     "+this.GetComponent<Rigidbody> ().angularVelocity);
		if(state == 1){
			this.transform.Rotate (0f, 0f, speed * Time.deltaTime);
		}else if(state == 2){
			
		}else if(state == 0){
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = this.GetComponent<Rigidbody> ().useGravity;
//		this.transform.GetChild (2).GetComponent<Rigidbody> ().isKinematic = this.GetComponent<Rigidbody> ().isKinematic;
			if(this.transform.position.y < 4f && this.transform.position.y > 1f && !isHitOther){
				RaycastHit hit;
				if (Physics.Raycast (this.transform.position, this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer("AISword");
						this.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("AISword");
					}
				}else if (Physics.Raycast (this.transform.position, -this.transform.right, out hit, 10f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer("AISword");
						this.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("AISword");
					}
				}else if (Physics.Raycast (this.transform.position, -this.transform.forward, out hit, 3.1f)) {
					print ("Ray    " + hit.transform.tag);
					if (hit.transform.tag == "Ground") {
						this.gameObject.layer = LayerMask.NameToLayer("AISword");
						this.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("AISword");
					}
				}
			}
		}
	}

	void OnCollisionStay(Collision other){
		if (other.transform.tag == "Player") {
			isHit = true;
		} else if(other.transform.tag == "SwordGround"){
			if (state == 0) {
				this.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}else{
			Debug.Log ("hit other");
			isHitOther = true;
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
