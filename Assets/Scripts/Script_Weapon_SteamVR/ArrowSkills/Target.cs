using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// Use this for initialization
	public GameObject TargetAI;
	private Transform[] hitPositions;
	private bool hasHit;
	void Awake () {
		// if(TargetAI == null){
			// TargetAI = GameObject.Find("LastAI");
			// TargetAI = GameObject.Find("Target1");
			TargetAI = GameObject.Find("LastAI");
			Debug.Log(TargetAI);
			hitPositions = TargetAI.GetComponent<ExplodeTarget_Arrow>().targets;
			Debug.Log("hitPos" + hitPositions);
	}

	public Transform[] getTargetPositions(){
		return hitPositions;
	}

	// Update is called once per frame

}
