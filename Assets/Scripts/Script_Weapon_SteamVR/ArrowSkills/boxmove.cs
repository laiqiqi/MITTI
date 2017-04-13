using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxmove : MonoBehaviour {
	float left, right;
	Vector3 move;

	public float speed;
	// Use this for initialization
	void Start () {
		left = -10;
		right = 10;
		move = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x <= left){
			transform.Translate(Vector3.right * Time.deltaTime * speed);
			move = Vector3.right;
		}else if(transform.position.x >= right){
			transform.Translate(Vector3.left * Time.deltaTime * speed);
			move = Vector3.left;
		}else{
			transform.Translate(move * Time.deltaTime * speed);
		}
	}
}
