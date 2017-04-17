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

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;

	// Use this for initialization
	void Start () {
		counter = 0;
		nextScriptIndex = counter+1;

		talkScript = new string[]{"Hit me!! Show me your power!!!",
								"Well, you don't know how to control yourself?",
								"aaaaa",
								"Now, hit me with the sword"};

		isEndTutor = true;
		isStartTutor = false;
		isCountDown = false;

		color = this.GetComponent<Renderer>().material.GetColor("_Color");

		canvasAnimator = canvas.GetComponent<Animator>();
		textAnimator = text.GetComponent<Animator>();

		NextTalkScript();
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveUpDown();
		LookAtPlayer();

		Tutorial();

		FadeOut();
		// Debug.Log(counter + " next: " + nextScriptIndex);
		// Debug.Log(isEndTutor);
	}

	void Tutorial(){
		if(counter == talkScript.Length){
				isEndTutor = true;
		}

		if(!isStartTutor && !isEndTutor){
			if(!isCountDown){
				isCountDown = true;
				StartCoroutine(CountDown(5f));
			}
		}
		else{ // Tutorial Start!!!
			if(counter == nextScriptIndex && !isEndTutor){
				StartCoroutine(CountDown(5f));
			}
		}
		nextScriptIndex = counter+1;
	}

	IEnumerator CountDown(float sec){
		yield return new WaitForSeconds(sec);
		isStartTutor = true;
		
		if(!isEndTutor){
			NextTalkScript();
		}
	}

	void NextTalkScript(){
		UpdateText(talkScript[counter]);
		counter++;
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
	}

	void FadeOut() {
		if(isEndTutor){
			if(this.GetComponent<Renderer>().material.GetColor("_Color").a > 0f){
			color.a -= 0.01f;
			this.GetComponent<Renderer>().material.SetColor("_Color", color);
			}
			else{
				Destroy(canvas.gameObject);
			}
		}
	}

	public void UpdateText(string updateText){
		StartCoroutine(PopUpControl());
		text.text = updateText;
	}

	IEnumerator PopUpControl() {
		canvasAnimator.SetBool("isStartPopUp", true);
		textAnimator.SetBool("isStartPopUp", true);

		yield return new WaitForSeconds(0.1f);

		canvasAnimator.SetBool("isStartPopUp", false);
		textAnimator.SetBool("isStartPopUp", false);
	}
}
