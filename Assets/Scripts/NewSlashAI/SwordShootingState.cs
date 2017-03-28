﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordShootingAIState : AIState {
	private readonly AITest AI;
	public string name{ get;}
	private float speed;
	private int subState;
	private List<GameObject> swordList;
	private float radius;
	private GameObject initialSword;
	private int swordQuantity;
	private float timeSpawn;
	private Vector3 initialPosition;

	public SwordShootingAIState(AITest statePatternAI){
		AI = statePatternAI;
	}

	public void StartState(){
		AI.currentState = AI.swordShootingAIState;
		speed = 200;
		subState = 0;
		radius = 2.5f;
		swordList = new List<GameObject> ();
		initialSword = AI.swordController [0].transform.GetChild (0).gameObject;
		swordQuantity = 6;
		timeSpawn = 0;
//		swordQuantity = Random.Range (2, 7);
//		initialSword.transform.parent = null;
//		GameObject test = GameObject.Instantiate (AI.swordController[0].transform.GetChild(0).gameObject,Vector3.zero, Quaternion.identity) as GameObject;
	}

	public void UpdateState(){
		AI.transform.LookAt (AI.player.transform);
		if (subState == 0) {
			Debug.Log ("000000000000");
			SubState0_2 ();
		} else if (subState == 1) {
			Debug.Log ("111111111111");
			SubState1 ();
//			foreach (GameObject sword in swordList) {

//			for(int i = swordList.Count-1; i >=0; i--){
//				swordList[i].transform.LookAt (2 * swordList[i].transform.position - AI.player.transform.position);
//				Vector3 pos1 = swordList[i].transform.position - AI.transform.position;
//				Vector3 pos2 = AI.transform.up*-radius;
//				Debug.Log ("pos1    "+pos1);
//				Debug.Log ("pos2    "+pos2);
//				if (i == swordList.Count-1 && 360f / (swordQuantity) - Vector3.Angle (pos1, pos2) < 0.5f) {
//					if (swordList.Count < swordQuantity) {
//						Vector3 relativePos = AI.transform.forward;
//						Quaternion rotation = Quaternion.LookRotation (relativePos);
//						GameObject newSword = GameObject.Instantiate (initialSword, AI.transform.position + AI.transform.up * -radius, Quaternion.identity) as GameObject;
//						newSword.transform.rotation = rotation;
//						newSword.transform.parent = AI.swordController [0].transform;
//						swordList.Add (newSword);
//						Debug.Log ("Angle      "+Vector3.Angle(pos1, pos2));
//					}
//				}
//			}
		} else if (subState == 2) {
//			AI.swordController [0].transform.Rotate (Vector3.up);
//			AI.swordController [0].transform.LookAt (AI.player.transform);
			Vector3 relativePos = AI.transform.up;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			AI.swordController [0].transform.rotation = Quaternion.RotateTowards (AI.swordController [0].transform.rotation, rotation, speed/2f * Time.deltaTime);
			if (Quaternion.Angle (AI.swordController [0].transform.rotation, rotation) < 1f) {
				subState = 3;
				initialPosition = initialSword.transform.position;
			}
		} else if (subState == 3) {
			AI.swordController [0].transform.Rotate (Vector3.up);
			foreach (GameObject sword in swordList) {
				sword.transform.LookAt (2 * sword.transform.position - AI.player.transform.position);
				if (Mathf.Abs (sword.transform.position.y - initialPosition.y) < 1) {
					sword.SetActive (true);
				}
			}
		}
	}

	public void EndState(){
		
	}

	public void StateChangeCondition(){

	}

	public void SubState0(){
		Vector3 relativePos = AI.transform.forward;
		Quaternion rotation = Quaternion.LookRotation (relativePos);

//		rotation = Quaternion.Euler (rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
		initialSword.transform.rotation = Quaternion.RotateTowards (initialSword.transform.rotation, rotation, speed * Time.deltaTime);

//		float distance = radius;
//		Vector3 target = -AI.transform.up.normalized * distance;
//		Vector3 target = AI.transform.position +new Vector3(0f, -5f*distance, 0f);
		Vector3 target = AI.transform.position + AI.transform.up*-radius;
		initialSword.transform.position = Vector3.MoveTowards (initialSword.transform.position, target, speed / 50f * Time.deltaTime);

//		Debug.Log ("Vector   "+Vector3.Distance (initialSword.transform.localPosition, target));
//		Debug.Log ("Quater   "+Quaternion.Angle (initialSword.transform.rotation, rotation));
		if (Vector3.Distance (initialSword.transform.position, target) < 1f
			&& Quaternion.Angle (initialSword.transform.rotation, rotation) < 1f) {
			subState = 1;
			swordList.Add(initialSword);
			AI.swordController [0].transform.rotation = AI.transform.rotation;
			initialSword.transform.parent = AI.swordController [0].transform;
//			initialSword.GetComponent<SwordFloatingSword>().state = 3;
		}
	}

	public void SubState0_2(){
//		Vector3 target = AI.transform.position + AI.transform.forward*-radius;
		Vector3 target = GetCircle(AI.transform.position, radius, 0);
		initialSword.transform.position = Vector3.MoveTowards (initialSword.transform.position, target, speed / 50f * Time.deltaTime);
		if (Vector3.Distance (initialSword.transform.position, target) < 0.1f){
			subState = 1;
			swordList.Add(initialSword);
//			AI.swordController [0].transform.rotation = AI.transform.rotation;
			initialSword.transform.parent = AI.swordController [0].transform;
			//			initialSword.GetComponent<SwordFloatingSword>().state = 3;
		}
	}

	public void SubState1(){
		for(int i = 1; i < swordQuantity; i++){
			Vector3 pos = GetCircle (AI.transform.position, radius, i * (360f / swordQuantity));

			Vector3 relativePos = -AI.transform.up;
			Quaternion rotation = Quaternion.LookRotation (relativePos);

			GameObject newSword = GameObject.Instantiate (initialSword, pos, rotation) as GameObject;
//			newSword.transform.rotation = rotation;
			newSword.transform.parent = AI.swordController [0].transform;
			newSword.SetActive (false);
			swordList.Add (newSword);
//			Debug.Log ("Angle      "+Vector3.Angle(pos1, pos2));
		}
		subState = 2;
	}

	Vector3 GetCircle(Vector3 center, float radius,float a){
		Debug.Log(a);
		float ang = a;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}
}