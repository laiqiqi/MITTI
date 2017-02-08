using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {
	private int collidetime = 0;
	public Transform hitPrefab;

	private Transform hitclone;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		if(collidetime < 1){
			ContactPoint contact = collision.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;
			Destroy(Instantiate(hitPrefab, pos, rot).gameObject, hitPrefab.GetComponent<ParticleSystem>().main.duration);
			Destroy(gameObject, 2);
		}
		collidetime++;
	}

}
