using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isCountDown;
	public GameObject canvas;
	public Text text;
	public PlaySound canvasSoundPlayer, windTutorSound;

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;

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
								"Now shoot at the minions. Put at the bow and hold arrow trigger.", //9
								"Good job, use you can use skills by touching the touch pad.", //10
								"Skilled-arrow must be fully charged to shoot", //11
								"They have cooldown time. Be aware of it.", //12
								"Let’s try another weapon, the legendary sword.", //13
								"Press the trigger button at your back again.", //14
								"The sword can imbue with the magic crystals to gain special power.", //15
								"The red crystal give the magic power absorption skill when hit.", //16
								"Use it to heal yourself.", //17
								"The blue crystal has great magic power.", //18
								"Diffuse it with the sword then it will become magic greatsword.", //19
								"It is so powerful that you have to hold it two-handed.", //20
								"A hit with the enchanted sword will deplete all the crystal power.", //21
								"Crystals have the cooldown time to recover its power, use it wisely.", //22
								"I hope this is the last time seeing you.", //23
								"Defeat you own creation.", //24
								"Bring peace to this land and you can rest in peace.", //25
								"GoodLuck."}; //26

		isEndTutor = false;
		isCountDown = false;

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

		// else{ // Tutorial Start!!!
			if(counter == nextScriptIndex && !isEndTutor){
				float sec = 0.2f;

				if (counter == 0) {
					sec = 0f;
				}
				else if (counter == 2) {
					sec = 10f;
				}
				else if (counter == talkScript.Length-1) {
					sec = 7f;
				}
				StartCoroutine(CountDown(sec));
			}
		// }

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
