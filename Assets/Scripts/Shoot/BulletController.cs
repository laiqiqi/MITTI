using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed;
	public GameObject[] ignoreObject;
	// Use this for initialization
	void Start () {
//		speed = 2;
		foreach( GameObject gameObject in ignoreObject){
			Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.forward* speed * Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) {
		// Debug.Log (other.transform.name);
		Destroy(this.gameObject);
	}

// 	void OnCollisionEnter (Collision other) {
// //		Debug.Log (other.transform.name);
// 		Destroy(this.gameObject);
// 	}
}
