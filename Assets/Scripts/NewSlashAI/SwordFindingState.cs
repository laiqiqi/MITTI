using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordFindingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private GameObject[] AISword;
	private GameObject swordTarget;
	private GameObject swordControllerTarget;
	private int subState;
	public List<AIState> choice{ get;set; }

	public SwordFindingAIState(AITest statePatternAI){
		AI = statePatternAI;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.swordFindingAIState;
		speed = 5;
		subState = 0;
		AISword = GameObject.FindGameObjectsWithTag ("AISword");
		swordTarget = null;
		foreach(GameObject sword in AISword){
			if(swordTarget == null || Vector3.Distance(AI.transform.position, sword.transform.position) < Vector3.Distance(AI.transform.position, swordTarget.transform.position)){
				swordTarget = sword;
			}
		}

		swordControllerTarget = null;
		foreach(GameObject sc in AI.swordController){
			if (swordControllerTarget == null || sc.transform.GetChild (0) == null) {
				swordControllerTarget = sc.gameObject;
			}
		}
			
			
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform.position);

		if (subState == 0) {
			AI.transform.position = Vector3.MoveTowards (AI.transform.position, swordTarget.transform.position, speed *Time.deltaTime);
			if(Vector3.Distance (AI.transform.position, swordTarget.transform.position) < 5f){
				subState = 1;
				swordTarget.GetComponent<Rigidbody> ().isKinematic = true;
				swordTarget.GetComponent<Rigidbody> ().useGravity = false;
			}
		}else if(subState == 1){
			//swordController
			swordControllerTarget.transform.LookAt (swordTarget.transform);
			swordControllerTarget.transform.rotation = Quaternion.Euler (0f, swordControllerTarget.transform.rotation.eulerAngles.y, 0f);

			//sword
//			Vector3 relativePos = swordControllerTarget.transform.position - swordTarget.transform.position + swordControllerTarget.transform.forward * 5f;
			Vector3 relativePos = -Vector3.up;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			swordTarget.transform.rotation = Quaternion.RotateTowards (swordTarget.transform.rotation, rotation, speed*10 * Time.deltaTime);

			if(Vector3.Distance (AI.transform.position, swordTarget.transform.position) > AI.AIAndSwordDistance){
				swordTarget.transform.position = Vector3.MoveTowards (swordTarget.transform.position, AI.transform.position, speed/2 * Time.deltaTime);	
			}
//			swordTarget.transform.position = Vector3.MoveTowards (swordTarget.transform.position, AI.transform.position, speed * Time.deltaTime);
			if(Vector3.Distance (AI.transform.position, swordTarget.transform.position) <= AI.AIAndSwordDistance && Quaternion.Angle(swordTarget.transform.rotation, rotation) < 0.1f){
				subState = 2;
				swordTarget.transform.parent = swordControllerTarget.transform;
				swordTarget.GetComponent<SwordFloatingSword> ().state = 2;
				EndState ();
			}
		}
//		StateChangeCondition ();
	}

	public void EndState(){
//		AI.GetComponent<Rigidbody>().isKinematic = false;
//		AI.swordPullingAIState.StartState ();
//		AI.swordFloatingAIState.StartState ();
		AI.swordShootingAIState.StartState ();
	}

	public void StateChangeCondition(){
		if (Vector3.Distance (AI.transform.position, swordTarget.transform.position) < 5f) {
			EndState ();
		}
	}
}
