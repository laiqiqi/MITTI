using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BodyCollision : MonoBehaviour {
	private StatePatternAI AI;
	private GameObject player;
	[HideInInspector] public GameObject hitPos;

	void Start() {
		hitPos = null;
		AI = this.transform.GetComponentInParent<StatePatternAI>();
		player = Player.instance.gameObject;
		Debug.Log(player.name);
	}

	public void OnTriggerEnter(Collider col){
		// AI.bodyColInfo = col;
		if(AI.currentState == AI.slamState){
			if(col.tag == "Player"){
				Debug.Log("Slam Player");

				if(hitPos == null){
					hitPos = new GameObject();
					hitPos.name = "HitPos";
					hitPos.transform.position = player.transform.position;
					hitPos.transform.SetParent(AI.transform);
				}

				player.GetComponent<PlayerStat>().health -= 2f;
				player.transform.position = hitPos.transform.position + (AI.transform.forward + (Vector3.up*1.5f));
			}
		}
	}
}
