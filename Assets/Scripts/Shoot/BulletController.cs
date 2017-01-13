using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
//		speed = 2;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.forward* speed * Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) {
		Destroy(this.gameObject);
	}
}
