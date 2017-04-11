using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor;
	// Use this for initialization
	void Start () {
		isEndTutor = false;
	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
	}

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		if(col.Equals("SkillSword")){
			isEndTutor = true;
		}
	}
}
