using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextArrowCollider : MonoBehaviour {

	public GameObject AITutor;
	public bool isNext, isBack;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter (Collider col) {
		if(col.tag.Equals("projectile") || col.tag.Equals("playersword")){
			if(isNext){
				AITutor.GetComponent<TutorialAI>().Next();
			}
			else if(isBack){
				AITutor.GetComponent<TutorialAI>().Back();
			}
		}
	}
	
}
