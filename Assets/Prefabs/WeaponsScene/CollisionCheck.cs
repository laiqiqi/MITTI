using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {
	private int collidetime = 0;
	public Transform hitPrefab;

	public float arrowStuckDepth;
	private Transform hitclone;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Stick (GameObject hitObject) {
		GetComponent<Rigidbody>().isKinematic = true;
		Destroy(GetComponent<BoxCollider>()); 
		transform.Translate(arrowStuckDepth * Vector3.forward); // move the arrow deep inside 
		transform.parent = hitObject.transform; 
	}

	void OnCollisionEnter(Collision collision){
		if(collidetime < 1){
			if (collision.gameObject.tag == "AI"){
				ContactPoint contact = collision.contacts[0];
				Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
				Vector3 pos = contact.point;
				Destroy(Instantiate(hitPrefab, pos, rot).gameObject, hitPrefab.GetComponent<ParticleSystem>().main.duration);
				Stick(collision.gameObject);
				Destroy(gameObject, 2);
			}
		}
		collidetime++;
	}

}
