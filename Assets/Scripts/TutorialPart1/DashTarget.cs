using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTarget : MonoBehaviour {

	public GameObject tutorPart1Con;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter (Collider col){
		if(col.tag.Equals("Player")){
			tutorPart1Con.GetComponent<TutorPartOneGameController>().dashCount += 1;
			Destroy(this.gameObject);
		}
	}
}
