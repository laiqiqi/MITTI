using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

	// Use this for initialization
	public GameObject tutorialAI;
	void Start () {
		
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log(col.collider.tag);
		if(col.collider.tag.Equals("projectile")){
			Debug.Log("Arrow hit minion");
			tutorialAI.GetComponent<TutorialAI>().hitCounter++;
			// Destroy(transform.parent.gameObject);
		}
	}
}
