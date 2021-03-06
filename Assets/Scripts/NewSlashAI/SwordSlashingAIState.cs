﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;

public class SwordSlashingAIState : AIState {
	private readonly StatePatternAI AI;
	public string name{ get;}
	private float speed;
	private float agile;
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
	private Vector3 targetPosition;
	private float playerRadius = 5;
	private Transform circleCenter;
	private Vector3 AIRandomForward;
	private float timeToStop;
	public List<AIState> choice{ get;set; }
	public float stateDelay{ get;set;}

	public SwordSlashingAIState(StatePatternAI statePatternAI){
		AI = statePatternAI;
		target = new GameObject ().transform;
//		circleCenter = new GameObject ().transform;
		choice = new List<AIState>();
	}

	public void StartState(){
		AI.currentState = AI.swordSlashingAIState;
		speed = 400;
		agile = 200;
		if(AI.isRage){
//			speed = 400;
			agile = 300;
		}
		timecount = 1;
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
			AI.AISword.transform.parent = sc.transform;
			Transform sword = sc.transform.GetChild (0);
//			GameObject sword = AI.AISword;
			sc.GetComponent<AISwordController> ().state = 0;
			sc.transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
//			sword.transform.localPosition = new Vector3 (0f, 0f, 1f);
			sword.transform.localPosition = AI.transform.forward;
			sword.transform.rotation = Quaternion.Euler (new Vector3 (90f, 0f, 0f));
			sword.GetComponent<AISword> ().setHide ();
			sword.gameObject.SetActive (true);
			sword.GetComponent<Rigidbody> ().isKinematic = true;
			sword.GetComponent<AISword> ().state = 6;

			swords [i] = sword.gameObject;
			oldVelocity [i] = 0;
			countState [i] = 0;
			isStart [i] = false;
			i++;
		}

//		AI.AISword.SetActive (true);
//		AI.AISword.GetComponent<AISword> ().setHide ();
//		AI.AISword.transform.localPosition = new Vector3 (0f, 0f, 1f);
//		AI.AISword.transform.rotation = Quaternion.Euler (new Vector3 (90f, 0f, 0f));
//		AI.AISword.GetComponent<AISword> ().effect.GetComponent<ParticleSystem> ().loop = false;
	}

	public void UpdateState(){
		// Vector3 pos = AI.player.transform.position;
		// pos.y = AI.player.GetComponent<Player> ().bodyCollider.GetComponent<CapsuleCollider> ().height / 2f;
		// pos.y = AI.player.transform.position.y /2f;
//		Transform tran;
		// target.position = pos;
		// AI.transform.LookAt (target);
		AI.transform.LookAt (AI.player.transform);
		int i = 0;
		foreach(GameObject sc in AI.swordController){
			int state = sc.GetComponent<AISwordController> ().state;
//			Debug.Log (state);
			if (state == 0) {
//				Debug.Log ("state     0");
				//Rotate sword
				SubState0(sc, i);
			} else if (state == 1) {
//				Debug.Log ("state     1");
				//Prepare
				SubState1(sc, i);
			} else if (state == 2) {
//				Debug.Log ("state     2");
				//Connect with joint
				SubState2(sc, i);

			} else if (state == 3) {
				// Debug.Log ("state     3");
				//Slash
				Substate3 (sc, i);
//				ConditionOfSubstate4(sc, i);

			} else if (state == 4) {
				// Debug.Log ("state     4");
				//Prepare for new slashing
				Substate4(sc, i);
			} else if (state == 5) {
				Substate5(sc, i);
//				Substate6 (sc, i);

			} else if (state == 6) {
				Substate6(sc, i);

			} else if (state == 7) {
				Substate7(sc, i);

			} else if (state == 8) {
//				Debug.Log ("state    8");
				Substate8(sc, i);

			}else if (state == -2) {
				SubstateMoveInCircle(sc, i);

			} else if (state == -1) {
//				Debug.Log ("state     -1");
				//OnJoinBreak
			 	swords [i].GetComponent<Rigidbody> ().useGravity = true;
				swords [i].GetComponent<AISword> ().state = 0;
//				swords [i].transform.GetChild (2).GetComponent<Rigidbody> ().useGravity = true;
			}
//			Debug.Log ("hitcount    " + hitCount);
			if (hitCount >= 5) {
				sc.GetComponent<AISwordController> ().state = 8;
				hitCount = 0;
			}
//			if(Vector3.Distance (AI.transform.position, AI.player.transform.position) < playerRadius){
//				circleCenter.position = AI.player.transform.position;
//				AI.transform.parent = circleCenter.transform;
//				circleCenter.transform.Rotate (0f, 1f, 0f);
//			}
			//Debug
//	 		Debug.Log (randomVector[i]);
//			Debug.Log (AI.transform.forwards);
//			Debug.DrawLine (swords[i].transform.position, Vector3.Cross(AI.transform.forward, swords[i].transform.forward),Color.red);
//			Debug.DrawLine (swords[i].transform.position, randomVector[i],Color.red);
//			Debug.DrawLine (swords[i].transform.position, -AI.transform.forward + AI.transform.position,Color.red);
			i++;
		}
	}

	public void EndState(){
		AI.GetComponent<Rigidbody>().isKinematic = false;

//		sc.AddComponent<FixedJoint> ();
	}

	public void StateChangeCondition(){

	}

//	public void SubStateSpawnSword(GameObject sc, int i){
//		if (swords [i].GetComponent<>) {
//		
//		}
//	}

	public void SubState0(GameObject sc, int i){
		//old
//		if(swords[i].GetComponent<AISword>().state != 6){
//			swords[i].GetComponent<AISword>().state = 6;	
//		}
//		Vector3 heading = swords [i].transform.position - AI.transform.position;
//		float angle = Vector3.Angle (heading, -AI.transform.forward);

//		if (Mathf.Abs (angle) < 20 ) {
//			sc.GetComponent<AISwordController> ().state = 1;
//		}
		if(swords [i].GetComponent<AISword>().swordModel.GetComponent<FadeManager>().isShow) {
			sc.GetComponent<AISwordController> ().state = 1;
			swords[i].GetComponent<Rigidbody> ().isKinematic = true;
			dir =(int)Mathf.Pow (-1, Random.Range (1, 3));
		}

//		sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
	}

	public void SubState1(GameObject sc, int i){
//		sc.transform.rotation = Quaternion.Euler (new Vector3(0, 0, 0));
		Vector3 relativePos = sc.transform.position - swords [i].transform.position;
//		Vector3 relativePos = AI.transform.right - swords [i].transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);

//		rotation = Quaternion.Euler (rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
//		relativePos = AI.transform.right - swords [i].transform.position;
		swords [i].transform.rotation = Quaternion.RotateTowards (swords [i].transform.rotation, rotation, agile * Time.deltaTime);

		float distance = 3f;
//		dir = (int)Mathf.Pow (-1, Random.Range (1, 3));
//		dir = 1;
//		randomVector [i] = AI.transform.position + AI.transform.right * -dir;
		randomVector[i] = AI.transform.right *distance*dir + AI.transform.position - AI.transform.forward;
		swords [i].transform.position = Vector3.MoveTowards (swords [i].transform.position, randomVector [i], agile / 50f * Time.deltaTime);
//		Debug.DrawLine (swords [i].transform.position,randomVector [i], Color.red);

		if (Vector3.Distance (swords [i].transform.position, randomVector [i]) < 0.01f
			&& Quaternion.Angle (swords [i].transform.rotation, rotation) < 0.01f) {
//			if (Vector3.Angle (swords [i].transform.up, Vector3.up) > Vector3.Angle (swords [i].transform.up, -Vector3.up)) {
////				dir = -dir;
//			}
			dir = -dir;
			sc.GetComponent<AISwordController> ().state = 2;
		}
	}

	public void SubState2(GameObject sc, int i){
		if (sc.transform.childCount != 0) {
			swords [i].transform.parent = null;
			swords [i].GetComponent<AISword> ().state = 2;
			sc.transform.LookAt (swords [i].transform);
			sc.GetComponent<AISwordController> ().state = 4;
			if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > playerRadius) {
				sc.GetComponent<AISwordController> ().state = 6;
			}

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
		swords [i].GetComponent<MeleeWeaponTrail> ().Emit = true;
		timecount += Time.deltaTime;
		if (timecount > 5f) {
//			Debug.Log ("Time exceed to the limit");
//			timecount = 10f;
			sc.GetComponent<AISwordController> ().state = 8;
		}

		if (swords [i].GetComponent<AISword> ().isHit) {
//			timecount /= 2;
		}
//		int direction = -Mathf.RoundToInt ((randomVector [i].x - AI.transform.position.x) / Mathf.Abs (randomVector [i].x - AI.transform.position.x));

		sc.GetComponent<Rigidbody> ().AddTorque (sc.transform.up * 400 * dir);
//		swords [i].GetComponent<MeleeWeaponTrail> ().Emit = true;

		if (sc.GetComponent<Rigidbody> ().angularVelocity.magnitude > 1.5f) {
			isStart[i] = true;
		}
		if (swords[i].GetComponent<AISword>().isHit) {
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
		if((countState[i] == 2 && isStart[i] && !swords[i].GetComponent<AISword>().isHit)){
			isStart [i] = false;
			RandomVectorForSlashing (sc, i);
			hitCount++;
//			Debug.Log (hitCount);
//			Debug.Log("Count state == 2");
			sc.GetComponent<AISwordController> ().state = 4;
		}

		if (swords[i].GetComponent<AISword>().isHitOther) {
			isStart [i] = false;
			RandomVectorForSlashing (sc, i);
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
			RandomVectorForSlashing (sc, i);
			sc.GetComponent<AISwordController> ().state = 4;
		}
			
		if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > playerRadius) {
			sc.GetComponent<AISwordController> ().state = 8;
		}
		
//		if (timecount > 3) {
		Vector2 v1 = new Vector2 (AI.transform.position.x, AI.transform.position.z);
		Vector2 v2 = new Vector2 (AI.player.transform.position.x, AI.player.transform.position.z);
		if (Vector2.Distance (v1, v2) < 1f || Vector3.Distance (AI.transform.position, AI.player.transform.position) < 3f) {
			if (timecount < 0) {
				sc.GetComponent<AISwordController> ().state = 8;
			}
			timecount -= Time.deltaTime * 3f;
//			}
		}
	}

	public void Substate4(GameObject sc, int i){
		swords [i].GetComponent<MeleeWeaponTrail> ().Emit = false;
//		randomVector[i] = AI.transform.position + AIRandomForward;
		Vector3 relativePos = randomVector[i] - sc.transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
//		sc.transform.rotation = Quaternion.RotateTowards (sc.transform.rotation, rotation, speed*6 * Time.deltaTime);
//		float distance = Vector3.Distance();
		sc.transform.rotation = Quaternion.RotateTowards (sc.transform.rotation, rotation, agile*10 * Time.deltaTime);
		if (Quaternion.Angle (sc.transform.rotation, rotation) < 0.1f) {
			swords [i].GetComponent<AISword> ().swordSwipeSound.Play ();
			sc.GetComponent<AISwordController> ().state = 3;
			timecount = 0;
//			sc.GetComponent<Rigidbody> ().isKinematic = true;
//			timeToStop = 0;
		}

		if (swords[i].GetComponent<AISword>().isHitOther) {
//			Debug.Log ("State 4 hitother");
			RandomVectorForSlashing (sc, i);
			if(swords [i].GetComponent<AISword> ().isHitOther){
				swords [i].GetComponent<AISword> ().isHitOther = false;
			}
		}

		if (swords [i].GetComponent<AISword> ().isHit) {
			sc.GetComponent<AISwordController> ().state = 8;
		}
	}

	public void Substate5(GameObject sc, int i){
		// move sword to the back of ai
//		swords[i].GetComponent<Rigidbody>().isKinematic = true;
		swords [i].GetComponent<MeleeWeaponTrail> ().Emit = false;
//		randomVector [i] = AI.transform.position + -AI.transform.forward;
		Vector3 relativePos = AI.transform.right - sc.transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		sc.transform.rotation = Quaternion.RotateTowards (sc.transform.rotation, rotation, agile*10 * Time.deltaTime);
		if (Quaternion.Angle (sc.transform.rotation, rotation) < 0.01f) {
			sc.GetComponent<AISwordController> ().state = 6;
		}
	}

	public void Substate6(GameObject sc, int i){
		// move to player
		sc.GetComponent<Rigidbody>().isKinematic = true;
		Vector3 playerPos = AI.player.transform.position;
		playerPos.y = -0.2f;
		AI.transform.position = Vector3.MoveTowards (AI.transform.position, playerPos, agile/10f * Time.deltaTime);
		if (Vector3.Distance (AI.transform.position, AI.player.transform.position) < playerRadius) {
			sc.GetComponent<Rigidbody>().isKinematic = false;
			swords [i].GetComponent<AISword> ().swordSwipeSound.Play ();
			sc.GetComponent<AISwordController> ().state = 3;
			timecount = 0;
		}
	}

	public void Substate7(GameObject sc, int i){
		//move sword to the back of ai with vertical movement
		Vector3 heading = swords [i].transform.position - AI.transform.position;
		float angle = Vector3.Angle (heading, -AI.transform.forward);

		if (Mathf.Abs (angle) < 20 ) {
			sc.GetComponent<AISwordController> ().state = 1;
		}
		sc.transform.Rotate (0f, speed * Time.deltaTime, 0f);
	}

	public void Substate8(GameObject sc, int i){
		//hide sword 
//		Vector3 playerPos = AI.player.transform.position;
//		playerPos.y = AI.transform.position.y;
//		Debug.Log("88888888888888888888888");
		swords [i].GetComponent<MeleeWeaponTrail> ().Emit = false;
		AI.transform.position += -AI.transform.forward*Time.deltaTime*10;
//		AI.transform.position = Vector3.MoveTowards (AI.transform.position, playerPos, speed/10f * Time.deltaTime);
		if (Vector3.Distance (AI.transform.position, AI.player.transform.position) > 2*playerRadius) {
			sc.GetComponent<AISwordController> ().state = 10;
			swords[i].GetComponent<AISword> ().state = 6;
			AI.NextState ();
			GameObject.Destroy (sc.GetComponent<FixedJoint> ());

//			swords[i].gameObject.SetActive (false);
//			Debug.Log ("ssssss"+sc.GetComponent<AISwordController> ().state);
		}
	}

	public void SubstateMoveInCircle(GameObject sc, int i){
		Vector3 v = AI.transform.position - AI.player.transform.position;
		float degreesPerSecond = 60f;
		v = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.up) * v;
		AI.transform.position = AI.player.transform.position + v;

	}

	public void RandomVectorForSlashing(GameObject sc, int i){
		dir = (int)Mathf.Pow (-1, Random.Range (1, 3));
		Vector3 AIRandomForward = -AI.transform.forward + AI.transform.right * Random.Range (1f, 5f) * -dir + AI.transform.up * Random.Range (0f, 5f);
		randomVector[i] = AI.transform.position + AIRandomForward;
	}

}
