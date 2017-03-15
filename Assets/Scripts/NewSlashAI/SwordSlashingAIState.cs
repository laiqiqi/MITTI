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
	private bool[] isStart;
	private int[] countState;
	private float[] oldVelocity;
	private Quaternion rotationTemp;
	private int dir;
	private int hitCount;

	public SwordSlashingAIState(AITest statePatternAI){
		AI = statePatternAI;
		target = new GameObject ().transform;
	}

	public void StartState(){
		AI.currentState = AI.swordSlashingAIState;
		speed = 200;
		timecount = 0;
		dir = 1;
		hitCount = 0;
		countState = new int[AI.swordController.Length];
		oldVelocity = new float[AI.swordController.Length];
		swords = new GameObject[AI.swordController.Length];
		randomVector = new Vector3[AI.swordController.Length];
		isStart = new bool[AI.swordController.Length];
		int i = 0;
		foreach(GameObject sc in AI.swordController){
//			randomVector[i] = new Vector3 (Random.Range(-5f,5f), Random.Range(0f,5f), Random.Range(2f,5f));
			RandomVectorForSlashing(sc ,i);
			Transform sword = sc.transform.GetChild (0);
			swords [i] = sword.gameObject;
			oldVelocity [i] = 0;
			countState [i] = 0;
			isStart [i] = false;
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
				SubState0(sc, i);
			} else if (state == 1) {
				Debug.Log ("state     1");
				//Prepare
				SubState1(sc, i);
			} else if (state == 2) {
				Debug.Log ("state     2");
				//Connect with joint
				SubState2(sc, i);

			} else if (state == 3) {
				Debug.Log ("state     3");
				//Slash
				Substate3 (sc, i);
//				ConditionOfSubstate4(sc, i);

			} else if (state == 4) {
				Debug.Log ("state     4");
				//Prepare for new slashing
				Substate4(sc, i);
			} else if (state == -1) {
				Debug.Log ("state     -1");
				//OnJoinBreak
			 	swords [i].GetComponent<Rigidbody> ().useGravity = true;
//				swords [i].transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = true;
			}
			//Debug
//	 		Debug.Log (randomVector[i]);
//			Debug.Log (AI.transform.forwards);
//			Debug.DrawLine (swords[i].transform.position, Vector3.Cross(AI.transform.forward, swords[i].transform.forward),Color.red);
//			Debug.DrawLine (swords[i].transform.position, randomVector[i],Color.red);
			Debug.DrawLine (swords[i].transform.position, -AI.transform.forward + AI.transform.position,Color.red);
			i++;
		}
	}

	public void EndState(){
		AI.GetComponent<Rigidbody>().isKinematic = false;
	}

	public void StateChangeCondition(){

	}

	public void SubState0(GameObject sc, int i){
		Vector3 heading = swords [i].transform.position - AI.transform.position;
		float angle = Vector3.Angle (heading, -AI.transform.forward);

		if (Mathf.Abs (angle) < 10) {
			sc.GetComponent<AISwordController> ().state = 1;
		}

		sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
	}

	public void SubState1(GameObject sc, int i){
		Vector3 relativePos = sc.transform.position - swords [i].transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);

		rotation = Quaternion.Euler (rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
		swords [i].transform.rotation = Quaternion.RotateTowards (swords [i].transform.rotation, rotation, speed * Time.deltaTime);

		float distance = 3f;
		swords [i].transform.localPosition = Vector3.MoveTowards (swords [i].transform.localPosition, randomVector [i].normalized * distance, speed / 50f * Time.deltaTime);
		if (Vector3.Distance (swords [i].transform.localPosition, randomVector [i].normalized * distance) < 1f
			&& Quaternion.Angle (swords [i].transform.rotation, rotation) < 1f) {

			sc.GetComponent<AISwordController> ().state = 2;
		}
	}

	public void SubState2(GameObject sc, int i){
		if (sc.transform.childCount != 0) {
			swords [i].transform.parent = null;
			swords [i].GetComponent<SwordFloatingSword> ().state = 2;
			sc.transform.LookAt (swords [i].transform);
			sc.GetComponent<AISwordController> ().state = 4;
			sc.AddComponent<FixedJoint> ();
			sc.GetComponent<FixedJoint> ().connectedBody = swords [i].GetComponent<Rigidbody> ();
//			sc.GetComponent<FixedJoint> ().breakForce = 500;
//			sc.GetComponent<FixedJoint> ().breakTorque = 500;
			swords [i].GetComponent<Rigidbody> ().isKinematic = false;
			//					Physics.IgnoreLayerCollision (8,8);
			for(int j=0; j < swords.Length; j++){
				if (j != i) {
					Physics.IgnoreCollision (swords[i].GetComponent<Collider>(), swords[j].GetComponent<Collider>());
					//							Physics.IgnoreCollision (swords[i].GetComponent<Collider>(), swords[j].GetComponent<Collider>());
				}
			}
			//					swords [i].transform.parent = sc.transform;
		}
	}

	public void Substate3(GameObject sc, int i){
		timecount += Time.deltaTime;
		if (timecount > 10f) {
			timecount = 10f;
		}

		if (swords [i].GetComponent<SwordFloatingSword> ().isHit) {
			timecount /= 2;
		}
		int direction = -Mathf.RoundToInt ((randomVector [i].x - AI.transform.position.x) / Mathf.Abs (randomVector [i].x - AI.transform.position.x));

		sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * timecount * 100 * dir);

		if (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 1.5f) {
			isStart[i] = true;
		}
		if (swords[i].GetComponent<SwordFloatingSword>().isHit) {
			isStart[i] = false;
		}
			
//		Debug.Log (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude - oldVelocity [i]);
//		Debug.Log(Mathf.Abs(oldVelocity [i] - sc.GetComponent<Rigidbody> ().angularVelocity.magnitude));
		if (oldVelocity [i] - sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 0.01f) {
			countState [i] = 1;
		} else if (Mathf.Abs(sc.GetComponent<Rigidbody> ().angularVelocity.magnitude - oldVelocity [i]) > 0.01f && countState [i] == 1) {
			countState [i] = 2;
		} else {
			countState [i] = 0;
		}

		oldVelocity[i] = sc.GetComponent<Rigidbody> ().angularVelocity.magnitude;
		if((countState[i] == 2 && isStart[i] && !swords[i].GetComponent<SwordFloatingSword>().isHit)){
			isStart [i] = false;
			RandomVectorForSlashing (sc, i);
			hitCount++;
//			Debug.Log (hitCount);
			Debug.Log("Count state == 2");
			sc.GetComponent<AISwordController> ().state = 4;
		}

		if (swords[i].GetComponent<SwordFloatingSword>().isHitOther) {
			isStart [i] = false;
			sc.GetComponent<AISwordController> ().state = 4;
		}

//		Vector3 relativePos =  -AI.transform.forward + AI.transform.position;
////		Vector3 relativePos = randomVector[i] - sc.transform.position;
//		Quaternion rotation = Quaternion.LookRotation (-relativePos);
//		Debug.Log ("Angle     "+Quaternion.Angle (sc.transform.rotation, rotation));
//		if ((Quaternion.Angle (sc.transform.rotation, rotation) < 20f)){// || Quaternion.Angle (sc.transform.rotation, rotation) > 178f) && isStart[i]) {
//			isStart [i] = false;
//			Debug.Log("Angle < 5f");
//			sc.GetComponent<AISwordController> ().state = 4;
//		}

		Vector3 heading = swords [i].transform.position - AI.transform.position;
		float angle = Vector3.Angle (heading, -AI.transform.forward);
//		Debug.Log ("angle         "+angle);
		if (Mathf.Abs (angle) < 20) {
			sc.GetComponent<AISwordController> ().state = 4;
		}
	}

	public void Substate4(GameObject sc, int i){
		Vector3 relativePos = randomVector[i] - sc.transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		sc.transform.rotation = Quaternion.RotateTowards (sc.transform.rotation, rotation, speed*6 * Time.deltaTime);
		if (Quaternion.Angle (sc.transform.rotation, rotation) < 0.1f) {
			sc.GetComponent<AISwordController> ().state = 3;
		}

		if (swords[i].GetComponent<SwordFloatingSword>().isHitOther) {
			RandomVectorForSlashing (sc, i);
		}
	}

	public void RandomVectorForSlashing(GameObject sc, int i){
		dir = (int)Mathf.Pow (-1, Random.Range (1, 3));
		Vector3 AIRandomForward = -AI.transform.forward + AI.transform.right * Random.Range (1f, 5f) * -dir + AI.transform.up * Random.Range (0f, 5f);
		randomVector[i] = AI.transform.position + AIRandomForward;
	}

}
