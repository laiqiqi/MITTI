using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SlamCollider : MonoBehaviour {
	private GameObject player;

	void Start() {
		player = Player.instance.gameObject;
	}
	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Wall" || collider.tag == "Floor") {
			Debug.Log(collider.tag);
			StatePatternAI.instance.slamState.isStop = true;
			if(player.GetComponent<PlayerStat>().isHitSlam == true){
				Debug.Log("Hit player against " + collider.tag);
				player.GetComponent<PlayerStat>().health -= 200f;
			}
			else{
				StatePatternAI.instance.slamState.isStun = true;
			}
		}
	}
}
