using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isStartTutor, isCountDown;
	public GameObject canvas;
	public Text text;

	private Color color;

	// Use this for initialization
	void Start () {
		isEndTutor = false;
		isStartTutor = false;
		isCountDown = false;
		color = this.GetComponent<Renderer>().material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
		LookAtPlayer();

		Tutorial();

		FadeOut();
	}

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		if(col.Equals("SkillSword")){
			isEndTutor = true;
		}
	}

	void LookAtPlayer(){
		transform.LookAt(Player.instance.transform);
		// canvas.transform.LookAt(Player.instance.transform);
	}

	void Tutorial(){
		if(!isStartTutor && !isEndTutor){
			if(!isCountDown){
				isCountDown = true;
				StartCoroutine(CountDown());
			}
		}
		else{
			text.text = "Well, you don't know how to control yourself?";
		}
	}

	IEnumerator CountDown(){
		yield return new WaitForSeconds(5f);
		isStartTutor = true;
	}

	void FadeOut() {
		if(isEndTutor){
			text.text = "Ouch!!";
			if(this.GetComponent<Renderer>().material.GetColor("_Color").a > 0f){
			color.a -= 0.01f;
			this.GetComponent<Renderer>().material.SetColor("_Color", color);
			}
			else{
				Destroy(canvas.gameObject);
			}
		}
	}
}
