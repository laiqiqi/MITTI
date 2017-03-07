using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectManager : MonoBehaviour {

	//For enable/disable effects
	public GameObject[] magicCircles;
	public GameObject[] tempCircles;
	//----------------------------------------//
	// 0 = stomp circle
	// 1 = dig circle
	// 2 = slam circle

	public GameObject[] skillEffects;
	public GameObject[] tempEffects;
	//----------------------------------------//
	// 0 = dirt blast
	// 1 = rock spike
	// 2 = slam collision

	// Use this for initialization
	void Awake () {
		tempCircles = new GameObject[magicCircles.Length];
		tempEffects = new GameObject[skillEffects.Length];
	}

	void Start () {
		
	}
//-----------MagicCircles-----------------------------------
	public void CreateStompCircle(Vector3 effectPos) {
		tempCircles[0] = (GameObject)Instantiate(magicCircles[0], effectPos, Quaternion.identity);
	}
	public void DestroyStompCircle() {
		Destroy(tempCircles[0]);
		tempCircles[0] = null;
	}

	public void CreateDigStrikeCircle(Vector3 effectPos) {
		tempCircles[1] = (GameObject)Instantiate(magicCircles[1], effectPos, Quaternion.identity);
	}
	public void DestroyDigStrikeCircle() {
		Destroy(tempCircles[1]);
		tempCircles[1] = null;
	}

	public void CreateSlamCircle(Vector3 effectPos) {
		tempCircles[2] = (GameObject)Instantiate(magicCircles[2], effectPos, Quaternion.identity);
	}
	public void UpdatePosSlamCircle(Vector3 effectPos, Vector3 playerPos) {
		tempCircles[2].transform.position = effectPos;
		tempCircles[2].transform.LookAt(playerPos);
	}
	public void DestroySlamCircle() {
		Destroy(tempCircles[2]);
		tempCircles[2] = null;
	}
//---------------------------------------------------------
//-----------SkillEffect-----------------------------------
	public void CreateDirtBlast(Vector3 effectPos) {
		tempEffects[0] = (GameObject)Instantiate(skillEffects[0], effectPos, Quaternion.identity);
	}
	public void DestroyDirtBlast() {
		Destroy(tempEffects[0]);
		tempEffects[0] = null;
	}

	public void CreateRockSpikeSummoner(Vector3 effectPos) {
		tempEffects[1] = (GameObject)Instantiate(skillEffects[1], effectPos, Quaternion.identity);
	}
	public void DestroyRockSpikeSummoner() {
		Destroy(tempEffects[1]);
		tempEffects[1] = null;
	}

	public GameObject CreateSlamCollider(Vector3 effectPos) {
		tempEffects[2] = (GameObject)Instantiate(skillEffects[2], effectPos, Quaternion.identity);
		return tempEffects[2];
	}
	public void DestroySlamCollider() {
		Destroy(tempEffects[2]);
		tempEffects[2] = null;
	}
//---------------------------------------------------------
}
