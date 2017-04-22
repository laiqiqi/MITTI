using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isTutor;
	public GameObject canvas;
	public Text text;
	public PlaySound canvasSoundPlayer, windTutorSound, tutorBGM;
	public GameObject[] minions;

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;
	public int hitCounter;

	// Use this for initialization
	void Start () {
		counter = -1;
		nextScriptIndex = counter+1;
		counter++;

		talkScript = new string[]{"Hello, Arcane transmutor.", //0
								"Hit me with the magic greatsword, if this is not the first time.", //1
								"It seem like you had lost all of your memory.", //2
								"I will show you how to use your power.", //3
								"Let’s start with special movement.", //4
								"You can press the touchpad to quickly dash forward.", //5
								"Now you know how to evade.", //6
								"Try pick up a bow from your back.", //7
								"Press a trigger button when your controller is at your back.", //8
								"Now shoot at the minions. Put arrow at the bow and hold trigger.", //9
								"Good job, you can use skills by touching the left touch pad.", //10
								"Skilled-arrow must be fully charged to shoot", //11
								"They have cooldown time. Be aware of it.", //12
								"Let’s try another weapon, the legendary sword.", //13
								"Press the trigger button at your back again.", //14
								"The sword can imbue with the magic crystals to gain special power.", //15
								"The red crystal give the magic power absorption skill when hit.", //16
								"Use it to heal yourself.", //17
								"The blue crystal has great magic power.", //18
								"Diffuse it with the sword then it will become magic greatsword.", //19
								"A hit with the enchanted sword will deplete all the crystal power.", //20
								"Crystals have the cooldown time to recover its power, use it wisely.", //21
								"I hope this is the last time seeing you.", //22
								"Defeat you own creation.", //23
								"Bring peace to this land and you can rest in peace.", //24
								"GoodLuck."}; //25

		isEndTutor = false;
		isTutor = false;

		color = this.GetComponent<Renderer>().material.GetColor("_Color");

		canvasAnimator = canvas.GetComponent<Animator>();
		textAnimator = text.GetComponent<Animator>();

		// NextTalkScript();
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

		// Tutorial Start!!!
		if(counter == nextScriptIndex && !isEndTutor){
			if (counter == 0) {
				// wait for <--- sec to go to ^ index
				StartCoroutine(CountDown(0f));
			}
			else if (counter == 2) {
				StartCoroutine(CountDown(1f));
			}
			else if (counter == 3) {
				isTutor = true;
				StartCoroutine(CountDown(0.2f));
			}
			else if (counter == 10) {
				foreach (GameObject minion in minions) {
					minion.SetActive(true);
				}
				if(hitCounter >= 2){
					StartCoroutine(CountDown(0f));
				}
			}
			else if (counter == talkScript.Length-1) {
				StartCoroutine(CountDown(7f));
				tutorBGM.isStartFadeOut = true;
			}
			else{
				StartCoroutine(CountDown(0.2f));
			}
		}

		if(counter == 10 && hitCounter >= 2){
			StartCoroutine(CountDown(0f));
			hitCounter = 0;
		}
		nextScriptIndex = counter+1;
	}

	IEnumerator CountDown(float sec){
		yield return new WaitForSeconds(sec);

		if(counter == talkScript.Length){
			isEndTutor = true;
		}

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
		if(col.tag.Equals("Sword") && !isTutor){
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
				foreach (GameObject minion in minions) {
					Destroy(minion);
				}

				windTutorSound.isStartFadeOut = true;
			}
		}
	}

	public void UpdateText(string updateText){
		StartCoroutine(PopUpControl());
		text.text = updateText;
	}

	IEnumerator PopUpControl() {
		canvasSoundPlayer.Play();

		canvasAnimator.SetBool("isStartPopUp", true);
		textAnimator.SetBool("isStartPopUp", true);

		yield return new WaitForSeconds(0.1f);

		canvasAnimator.SetBool("isStartPopUp", false);
		textAnimator.SetBool("isStartPopUp", false);
	}
}
