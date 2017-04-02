using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UltiBulletCollision : MonoBehaviour {
	void Start() {

	}
	void OnTriggerEnter(Collider col) {
		Debug.Log("Hit "+col.tag);
		if(col.tag.Equals("Player")){
			Player.instance.GetComponent<PlayerStat>().isStartDazzle = true;
			Destroy(this.gameObject);
		}
	}	
}
