using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isTutor, isUpdateText, isHitTarget, isCreateTarget;
	public GameObject canvas;
	public Text text;
	public PlaySound canvasSoundPlayer, windTutorSound, tutorBGM;
	public GameObject[] minions;
	public GameObject nextArrow, backArrow;
	public GameObject frontPic, rightPic;

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;
	public int hitCounter;
	public Sprite[] viveButtons;

	public GameObject queryChan;
	public GameObject dashTarget;

	// Use this for initialization
	void Start () {
		text.material.SetColor("_Color", Color.white);
		counter = -1;
		nextScriptIndex = counter+1;

		talkScript = new string[]{"Hello, Arcane transmutor.", //0
								"Shoot me with the arrow, if this is not the first time.", //1
								"It seem like you had lost all of your memory.", //2
								"I will show you how to use your power.", //3
								"Let’s start with special movement.", //4
								"You can press the both touchpad to quickly dash forward.", //5
								"Now you know how to evade.", //6
								"Try pick up a bow from your back.", //7
								"Press a trigger button when your controller is at your back.", //8
								"Shoot at the arrow to proceed. Put arrow at the bow and hold trigger.", //9
								"Good job, you can choose skills by touching the left touch pad.", //10
								"Skilled-arrow must be fully charged to shoot", //11
								"They have cooldown time. Be aware of it.", //12
								"Let’s try another weapon, the legendary sword.", //13
								"Press the trigger button at your back again.", //14
								"The sword can imbue with the magic crystals to gain special power.", //15
								"Use touch pad to select crystal and put in your sword.", //16
								"The red crystal give the magic power absorption skill when hit.", //17
								"Use it to heal yourself.", //18
								"The blue crystal has great magic power.", //19
								"Diffuse it with the sword then it will become magic greatsword.", //20
								"A hit with the enchanted sword will deplete all the crystal power.", //21
								"Crystals have the cooldown time to recover its power, use it wisely.", //22
								"I hope this is the last time seeing you.", //23
								"Defeat you own creation.", //24
								"Bring peace to this land and you can rest in peace.", //25
								"GoodLuck."}; //26

		isEndTutor = false;
		isTutor = false;
		isUpdateText = false;
		isHitTarget = false;

		color = this.GetComponent<Renderer>().material.GetColor("_Color");

		canvasAnimator = canvas.GetComponent<Animator>();
		textAnimator = text.GetComponent<Animator>();

		NextTalkScript();
	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
		LookAtPlayer();

		// AutoTutorial();
		Tutorial();

		FadeOut();
		// Debug.Log(counter + " next: " + nextScriptIndex);
		// Debug.Log(isEndTutor);
	}

	void Tutorial(){
		// Tutorial Start!!!
		if(counter < talkScript.Length && !isEndTutor){
			if (!isUpdateText) {
				// Debug.Log("isTutor "+isTutor);
				if (counter == 1) {
					//Wait for down sec to next
					StartCoroutine(CountDown(10f));
				}
				else if (counter == 2) {
					isTutor = true;
					StartCoroutine(CountDown(10f));
				}
				else if (counter == 3) {
					StartCoroutine(CountDown(5f));
				}
				else if (counter < 5) {
					StartCoroutine(CountDown(5f));
				}
				else if (counter == 5) {
					if(!isCreateTarget){
						dashTarget.SetActive(true);
						isCreateTarget = true;
					}
					if(isHitTarget){
						Debug.Log("Hit target");
						StartCoroutine(CountDown(0f));
					}
				}
				else if (counter == 6 || counter == 7 || counter == 8) {
					Debug.Log("In 6 7 8");
					StartCoroutine(CountDown(5f));
				}
				else if(counter == 9) {
					nextArrow.SetActive(true);
					backArrow.SetActive(false);
				}
				else if(counter == 10) {
					backArrow.SetActive(true);
				}
			}
		}
		// Debug.Log(counter);
	}

	// void AutoTutorial(){
	// 	// Tutorial Start!!!
	// 	if(counter == nextScriptIndex && !isEndTutor){
	// 		if (counter == 0) {
	// 			// wait for <--- sec to go to ^ index
	// 			StartCoroutine(CountDown(0f));
	// 		}
	// 		else if (counter == 2) {
	// 			StartCoroutine(CountDown(10f));
	// 		}
	// 		else if (counter == 3) {
	// 			isTutor = true;
	// 			StartCoroutine(CountDown(5f)); 
	// 		}
	// 		else if (counter == 10) {
	// 			foreach (GameObject minion in minions) {
	// 				minion.SetActive(true);
	// 			}
	// 			if(hitCounter >= 2){
	// 				StartCoroutine(CountDown(0f)); // teach arrow
	// 			}
	// 		}
	// 		else if (counter == talkScript.Length-1) {
	// 			StartCoroutine(CountDown(7f)); // last time
	// 		}
	// 		else{
	// 			StartCoroutine(CountDown(5f)); // others time
	// 		}
	// 	}

	// 	if(counter == 10 && hitCounter >= 2){
	// 		StartCoroutine(CountDown(0f));
	// 		hitCounter = 0;
	// 	}
	// 	nextScriptIndex = counter+1;
	// }

	void TutorPicControl() {
		if(counter == 5){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[2];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[2];
		}
		if(counter == 7){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[3];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[0];
		}
		if(counter == 9){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[0];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[3];
		}
		else if(counter == 10){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[1];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[2];
		}
		else if(counter == 14){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[3];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[0];
		}
		else if(counter == 16){
			rightPic.GetComponent<SpriteRenderer>().sprite = viveButtons[1];
			frontPic.GetComponent<SpriteRenderer>().sprite = viveButtons[2];
		}
	}

	IEnumerator CountDown(float sec){
		isUpdateText = true;

		yield return new WaitForSeconds(sec);

		if(counter+1 == talkScript.Length){
			isEndTutor = true;
		}

		if(!isEndTutor){
			NextTalkScript();
		}
	}

	// IEnumerator WaitAndNextTalk(float sec){
	// 	isWaitToTalk = true;
	// 	if(counter+1 == talkScript.Length){
	// 		isEndTutor = true;
	// 	}
	// 	yield return new WaitForSeconds(sec);
	// 	counter++;
	// }

	void NextTalkScript(){
		counter++;
		UpdateText(talkScript[counter]);
		TutorPicControl();
		isUpdateText = false;
	}

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log(isTutor);
		if (col.tag.Equals("projectile") && !isTutor) {
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
				tutorBGM.isStartFadeOut = true;
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

	public void Next () {
		if(counter+1 < talkScript.Length){
			counter++;
			UpdateText(talkScript[counter]);
			TutorPicControl();
		}
		else{
			isEndTutor = true;
		}
	}

	public void Back () {
		if(counter-1 > 0){
			counter--;
			UpdateText(talkScript[counter]);
			TutorPicControl();
		}
	}
}
