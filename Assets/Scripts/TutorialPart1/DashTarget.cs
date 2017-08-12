using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTarget : MonoBehaviour {

	public TutorialAIStatePattern tutorAI;
	public bool isPass = false;
	public GameObject light, effect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter (Collider col){
		if(col.tag.Equals("Player")){
			tutorAI.dashCount += 1;
			isPass = true;
			// Destroy(this.gameObject);
			this.gameObject.SetActive(false);
		}
	}

	public void TargetActive() {
		if(!isPass){
			this.GetComponent<MeshRenderer>().enabled = true;
			this.GetComponent<BoxCollider>().enabled = true;
			light.SetActive(true);
			effect.SetActive(true);
		}
	}

	public void TargetDisable(){
		this.GetComponent<MeshRenderer>().enabled = false;
		this.GetComponent<BoxCollider>().enabled = false;
		light.SetActive(false);
		effect.SetActive(false);
	}
}
