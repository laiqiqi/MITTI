using UnityEngine;
using System.Collections;

public class SwordSlashingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private Vector3[] randomVector;
	private int subState;
	private float timecount;
	private GameObject[] swords;
	private Transform target;

	public SwordSlashingAIState(AITest statePatternAI){
		AI = statePatternAI;
		target = new GameObject ().transform;
	}

	public void StartState(){
		AI.currentState = AI.swordSlashingAIState;
		speed = 200;
		timecount = 0;
		swords = new GameObject[AI.swordController.Length];
		//		AI.currentState = AI.stopState;
		//		AI.GetComponent<Rigidbody>().isKinematic = true;

//		foreach(GameObject sc in AI.swordController){
////			sc.transform.GetChild (0).LookAt (sc.transform.position);
//			GameObject sword = sc.transform.GetChild (0);
//			sword.transform.rotation = Quaternion.RotateTowards(sword.transform.rotation, sword.transform.LookAt(sc.transform.position), speed);
//
////			sc.AddComponent<FixedJoint>();
////			sc.GetComponent<FixedJoint>().connectedBody = sc.transform.GetChild(0).GetComponent<Rigidbody>();
//
////			sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
//		}
		randomVector = new Vector3[AI.swordController.Length];
		for(int i = 0; i< AI.swordController.Length; i++){
			randomVector[i] = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
		}
//		randomVector = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
//		subState = 0;
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			//			sc.transform.GetChild (0).LookAt (sc.transform.position);
//			GameObject sword = sc.transform.GetChild (0);
//			sc.transform.GetChild (0).transform.rotation = Quaternion.RotateTowards(sc.transform.GetChild (0).transform.rotation, sc.transform.GetChild (0).transform.LookAt(sc.transform.position), speed);

			//			sc.AddComponent<FixedJoint>();
			//			sc.GetComponent<FixedJoint>().connectedBody = sc.transform.GetChild(0).GetComponent<Rigidbody>();

			//			sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
			int state = sc.GetComponent<AISwordController>().state;
//			if(Mathf.Abs(sc.transform.eulerAngles.y) > 170f){
//				state = 1;
//			}
			if (state == 0) {
				//Rotate sword
				Transform sword = sc.transform.GetChild (0);
				Vector3 heading = sword.transform.position - AI.transform.position;
				float angel = Vector3.Angle(heading, -AI.transform.forward);
				Debug.Log (Mathf.Abs (angel));
				if (Mathf.Abs (angel) < 10) {
					sc.GetComponent<AISwordController>().state = 1;
				}
					
				
				Debug.Log ("state     0");
				sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
//				if(Mathf.Abs(sc.transform.eulerAngles.y) > 170f){
//					state = 1;
//				}
//				sc.GetComponent<Rigidbody> ().AddTorque (-AI.transform.up * speed*Time.deltaTime);
			}else if(state == 1){
				Debug.Log ("state     1");
				//Prepare
				Transform sword = sc.transform.GetChild (0);
				Vector3 relativePos = sc.transform.position - sword.transform.position;
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				sword.rotation = Quaternion.RotateTowards(sword.rotation, rotation, speed*Time.deltaTime);

				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
				sword.localPosition = Vector3.MoveTowards (sword.localPosition, randomVector[i].normalized*distance, speed/50f*Time.deltaTime);
				if (Vector3.Distance (sword.localPosition, randomVector [i].normalized * distance) < 1f
					&& Quaternion.Angle(sword.rotation, rotation)< 1f) {
					sc.GetComponent<AISwordController>().state = 2;
				}

			}else if(state == 2){
				Debug.Log ("state     2");
				//Connect with joint
//				Transform sword = new GameObject().transform;
				if(sc.transform.childCount != 0){
					Transform sword = sc.transform.GetChild (0);
					sword.transform.parent = null;
					sword.GetComponent<SwordFloatingSword> ().state = 2;
					sc.transform.LookAt (sword.transform);
					sc.GetComponent<AISwordController>().state = 3;
					sc.AddComponent<FixedJoint> ();
					sc.GetComponent<FixedJoint> ().connectedBody = sword.GetComponent<Rigidbody> ();
//					sc.GetComponent<FixedJoint> ().breakForce = 500;
//					sc.GetComponent<FixedJoint> ().breakTorque = 500;
//					sword.GetComponent<Rigidbody> ().useGravity = true;
					sword.GetComponent<Rigidbody> ().isKinematic = false;
					swords [i] = sword.gameObject;
				}
//				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
//				sword.localPosition = Vector3.MoveTowards (sword.localPosition, randomVector[i].normalized*distance, speed/50f*Time.deltaTime);
			}else if(state == 3){
				Debug.Log ("state     3");
				//Slash
				if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > 5f) {
					AI.transform.position = Vector3.MoveTowards (AI.transform.position, AI.player.transform.position, 10 * Time.deltaTime);
				}

				timecount += Time.deltaTime;
//				Debug.Log (timecount);
				if (timecount > 10f) {
					timecount = 10f;
				}

				if(swords[i].GetComponent<SwordFloatingSword>().isHit){
					timecount /= 2;
				}

				int direction = Mathf.RoundToInt(randomVector[i].x/Mathf.Abs(randomVector[i].x));
//				if(randomVector[i].x < 0){
//					direction = -1;
//				}
				sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * timecount*100*direction);
//				Vector3 relativePos = AI.player.transform.position - sc.transform.position;
//				Quaternion rotation = Quaternion.LookRotation(relativePos);
//				sc.transform.rotation = Quaternion.RotateTowards(sc.transform.rotation, rotation, speed*Time.deltaTime);
//
//				float distance = Vector3.Distance (sc.transform.position, AI.player.transform.position);
//				swords[i].transform.position = Vector3.MoveTowards (swords[i].transform.position, randomVector[i].normalized*distance, speed/50f*Time.deltaTime);
			}
			i++;
		}
	}

	public void EndState(){
		AI.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}
}
