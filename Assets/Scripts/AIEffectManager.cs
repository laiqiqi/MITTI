using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectManager : MonoBehaviour {

	//For enable/disable effects sounds and animations
	public GameObject body;
	public GameObject[] magicCircles;
	private GameObject[] tempCircles;
	//----------------------------------------//
	// 0 = stomp circle
	// 1 = dig circle
	public GameObject[] skillEffects;
	private GameObject[] tempEffects;
	//----------------------------------------//
	// 0 = dirt blast

	[HideInInspector] public Animator bodyAnimator;

	// Use this for initialization
	void Start () {
		bodyAnimator = body.GetComponent<Animator>();
		tempCircles = new GameObject[magicCircles.Length];
		tempEffects = new GameObject[skillEffects.Length];
	}

//-----------Animation-------------------------------------
	public bool CheckBodyAnimState(int layer, string name){
		return bodyAnimator.GetCurrentAnimatorStateInfo(layer).IsName(name);
	}
	public void PlaySeekAnim() {
		bodyAnimator.SetBool("isSeek", true);
	}
	public void StopSeekAnim() {
		bodyAnimator.SetBool("isSeek", false);
	}

	public void PlayChargeStompAnim() {
		bodyAnimator.SetBool("isChargeStomp", true);
	}
	public void StopChargeStompAnim() {
		bodyAnimator.SetBool("isChargeStomp", false);
	}

	public void PlayChargeDigAnim() {
		bodyAnimator.SetBool("isChargeDig", true);
	}
	public void StopChargeDigAnim() {
		bodyAnimator.SetBool("isChargeDig", false);
	}
//---------------------------------------------------------

//-----------SkillEffect-----------------------------------
	public void CreateStompCircle(Vector3 effectPos) {
		tempCircles[0] = ((GameObject)Instantiate(magicCircles[0], effectPos, Quaternion.identity));
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

	public void CreateDirtBlast(Vector3 effectPos) {
		tempEffects[0] = (GameObject)Instantiate(skillEffects[0], effectPos, Quaternion.identity);
	}
	public void DestroyDirtBlast() {
		Destroy(tempEffects[0]);
		tempEffects[0] = null;
	}
//---------------------------------------------------------
}
