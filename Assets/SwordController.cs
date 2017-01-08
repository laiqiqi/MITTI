using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
	private bool state = true;
	private Vector3 direction;
	private float oldX;
	private float oldY;
	// Use this for initialization
	void Start () {
		direction = Vector3.up;
		transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
		oldX = 0;
		oldY = 0;
//		this.GetComponent<Rigidbody> ().AddForce (Vector3.forward*100);
	}

	// Update is called once per frame
	void Update () {
		if (state == true) {
			transform.Rotate (direction * 100f * Time.deltaTime);
		} else{
			transform.Rotate (-direction * 100f * Time.deltaTime);
		}
		if (Mathf.Abs (transform.rotation.y / 360f) < 10f) {
//			transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
		}
//		if (transform.rotation.y*oldY < 0) {
////			direction = new Vector3 (0f, 0f, Random.Range (0f, 1f));
////			direction = direction / Vector3.Magnitude (direction);
////			Debug.Log (Vector3.Magnitude (direction));
//			transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
//		}
		oldX = transform.rotation.x;
		oldY = transform.rotation.y;
	}

	void OnTriggerEnter(Collider collision) {
		state = !state;
	}
		
}
