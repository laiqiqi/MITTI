using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlamCollider : MonoBehaviour {
	[HideInInspector] public Collider colInfo;
	private GameObject player;

	void Start() {
		player = Player.instance.gameObject;
	}
	void OnTriggerEnter(Collider collider) {
		colInfo = collider;
		if (colInfo.tag == "Player") {
			if(player.GetComponent<PlayerControl>().isHitSlam){
				
			}
		}
	}
}
