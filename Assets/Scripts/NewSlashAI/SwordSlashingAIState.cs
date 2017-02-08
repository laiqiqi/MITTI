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
		randomVector = new Vector3[AI.swordController.Length];
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			randomVector[i] = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
			Transform sword = sc.transform.GetChild (0);
			swords [i] = sword.gameObject;
			i++;
		}

	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			int state = sc.GetComponent<AISwordController>().state;
			if (state == 0) {
				Debug.Log ("state     0");
				//Rotate sword
				Vector3 heading = swords [i].transform.position - AI.transform.position;
				float angel = Vector3.Angle (heading, -AI.transform.forward);

				if (Mathf.Abs (angel) < 10) {
					sc.GetComponent<AISwordController> ().state = 1;
				}

				sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);

			} else if (state == 1) {
				Debug.Log ("state     1");
				//Prepare
				Vector3 relativePos = sc.transform.position - swords [i].transform.position;
				Quaternion rotation = Quaternion.LookRotation (relativePos);
				swords [i].transform.rotation = Quaternion.RotateTowards (swords [i].transform.rotation, rotation, speed * Time.deltaTime);

//				float distance = Vector3.Distance (AI.transform.position, AI.player.transform.position);
				float distance = 3f;
				swords [i].transform.localPosition = Vector3.MoveTowards (swords [i].transform.localPosition, randomVector [i].normalized * distance, speed / 50f * Time.deltaTime);
				if (Vector3.Distance (swords [i].transform.localPosition, randomVector [i].normalized * distance) < 1f
				    && Quaternion.Angle (swords [i].transform.rotation, rotation) < 1f) {
					sc.GetComponent<AISwordController> ().state = 2;
				}

			} else if (state == 2) {
				Debug.Log ("state     2");
				//Connect with joint
				if (sc.transform.childCount != 0) {
					swords [i].transform.parent = null;
					swords [i].GetComponent<SwordFloatingSword> ().state = 2;
					sc.transform.LookAt (swords [i].transform);
					sc.GetComponent<AISwordController> ().state = 3;
					sc.AddComponent<FixedJoint> ();
					sc.GetComponent<FixedJoint> ().connectedBody = swords [i].GetComponent<Rigidbody> ();
//					sc.GetComponent<FixedJoint> ().breakForce = 500;
//					sc.GetComponent<FixedJoint> ().breakTorque = 500;
//					sword.GetComponent<Rigidbody> ().useGravity = true;
					swords [i].GetComponent<Rigidbody> ().isKinematic = false;

				}

			} else if (state == 3) {
				Debug.Log ("state     3");
				//Slash
				if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > 5f) {
					AI.transform.position = Vector3.MoveTowards (AI.transform.position, AI.player.transform.position, 10 * Time.deltaTime);
				}

				timecount += Time.deltaTime;
				Debug.Log (timecount);
				if (timecount > 10f) {
					timecount = 10f;
				}

				if (swords [i].GetComponent<SwordFloatingSword> ().isHit) {
					timecount /= 2;
				}

				int direction = Mathf.RoundToInt (randomVector [i].x / Mathf.Abs (randomVector [i].x));
				sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * timecount * 100 * direction);
//				sc.GetComponent<AISwordController> ().state = 4;
			} else if (state == 4) {

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
