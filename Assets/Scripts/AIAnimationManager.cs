using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationManager : MonoBehaviour {

	public GameObject body;
	[HideInInspector] public Animator bodyAnimator;

	// Use this for initialization
	void Start () {
		bodyAnimator = body.GetComponent<Animator>();
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

	public void PlayDigStrikeAnim() {
		bodyAnimator.SetBool("isDigStrike", true);
	}
	public void StopDigStrikeAnim() {
		bodyAnimator.SetBool("isDigStrike", false);
	}
//---------------------------------------------------------
}
