using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AIBodyCollision : MonoBehaviour {
	private StatePatternAI AI;
	private GameObject player;
	[HideInInspector] public GameObject hitPos;

	void Start() {
		hitPos = null;
		AI = StatePatternAI.instance.GetComponent<StatePatternAI>();
		player = Player.instance.gameObject;
		// Debug.Log(player.name);
	}

	public void OnTriggerEnter(Collider col){
		// AI.bodyColInfo = col;
		if(AI.currentState == AI.slamState){
			if(col.tag == "Player"){
				// if(hitPos == null){
				// 	hitPos = new GameObject();
				// 	hitPos.name = "HitPos";
				// 	hitPos.transform.position = player.transform.position;
				// 	hitPos.transform.SetParent(AI.transform);
				// }

				player.GetComponent<PlayerStat>().PlayerTakeDamage(40f);
				player.GetComponent<PlayerStat>().isHitSlam = true;
				player.GetComponent<Rigidbody>().isKinematic = false;
				player.GetComponent<Rigidbody>().AddForce(AI.transform.forward*5f + AI.transform.right*Random.Range(-5, 5f), ForceMode.Impulse);
			}
		}
		else{
			Destroy(hitPos);
		}
	}
}
