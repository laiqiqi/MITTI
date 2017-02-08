using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFloatingSword : MonoBehaviour {
	private float speed;
	public int state;
	public bool isHit;
	// Use this for initialization
	void Start () {
		speed = 300;
		isHit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == 1){
			this.transform.Rotate (0f, 0f, speed * Time.deltaTime);
		}else if(state == 2){
			
		}
	}

	void OnCollisionStay(Collision other){
		if (other.transform.tag == "Player") {
			isHit = true;
		}
	}

	void OnCollisionExit(Collision other){
		if (other.transform.tag == "Player") {
			isHit = false;
		}
	}
}
