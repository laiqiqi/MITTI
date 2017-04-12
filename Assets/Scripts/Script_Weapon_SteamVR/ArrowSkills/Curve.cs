using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
	private GameObject target1;
	public float timeToLerp;
	private float timeLerped = 0.0f;
	private Vector3 P0, P1, P2;
	// Use this for initialization
	void Start () {
		target1 = GameObject.Find("Target1");
		P0 = transform.position;
		P1 = new Vector3(Random.Range(5.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
		P2 = target1.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		P2 = target1.transform.position;
		timeLerped += Time.deltaTime;
		float t = timeLerped / timeToLerp;
		if (t >= 0 && t <= 1){
			transform.position = (1-t)*(1-t)*P0 + 2*(1-t)*t*P1 + t*t*P2;
			transform.LookAt(target1.transform);
		}
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.gameObject.tag == "AI"){
			Rigidbody box = gameObject.GetComponent<Rigidbody>();
			box.transform.parent = collision.collider.gameObject.transform;
			box.velocity = Vector3.zero;
			box.angularVelocity = Vector3.zero;
			box.isKinematic = true;
			box.useGravity = false;
			box.transform.GetComponent<BoxCollider>().enabled = false;
			StartCoroutine(fadeLineRenderer());
			// Destroy(gameObject);
		}
	}

	IEnumerator fadeLineRenderer(){
		TrailRenderer g = this.transform.GetComponent<TrailRenderer>();
		Debug.Log(g.time + "gtime");
		while(g.time > 0){
			g.time = g.time - 0.4f;
			Debug.Log(g.time);
			if(g.time == 0){

				Destroy(gameObject);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
