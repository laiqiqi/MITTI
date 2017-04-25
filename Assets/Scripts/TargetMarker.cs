using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TargetMarker : MonoBehaviour {

	public GameObject tutorAI;
	// Use this for initialization
	void Awake () {
		tutorAI.GetComponent<TutorialAI>().isHitTarget = false;
		Player.instance.GetComponent<PlayerControl>().isDashable = true;
		Debug.Log(Player.instance.GetComponent<PlayerControl>().isDashable);
	}

	void OnTriggerEnter (Collider col) {
		if(col.tag.Equals("Player") && Player.instance.GetComponent<PlayerControl>().isDashable){
			Debug.Log("PlayerEnter");
			tutorAI.GetComponent<TutorialAI>().isHitTarget = true;
			Player.instance.GetComponent<PlayerControl>().isDashable = false;
			Player.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Destroy(this.gameObject);
		}
	}

	void OnDestroy() {
        print("Script was destroyed");
    }
}
