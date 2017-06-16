using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialAIStatePattern : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isTutor, isUpdateText, isHitTarget, isCreateTarget;
	public GameObject canvas;
	public Text text;
	public PlaySound canvasSoundPlayer, tutorBGM;
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
	public GameObject[] dashTarget;

	public GameObject gameCon;

	[HideInInspector] public AIState currentState;
	[HideInInspector] public TutorDashState tutorDashState;

	// Use this for initialization
	void Start () {
		tutorDashState = new TutorDashState(this);

		text.material.SetColor("_Color", Color.white);
		// counter = -1;
		// nextScriptIndex = counter+1;

		// talkScript = new string[]{"Hello, Arcane transmutor.", //0
		// 						"Shoot me with the arrow, if this is not the first time.", //1
		// 						"It seem like you had lost all of your memory.", //2
		// 						"I will show you how to use your power.", //3
		// 						"Look at the gauge around your feet.", //4
		// 						"The green one show your health and orange show your stamina.", //5
		// 						"Let’s start with special movement.", //6
		// 						"You can press any touchpad to quickly dash forward.", //7
		// 						"Go to yellow light that appear around.", //8
		// 						"Good job, now you know how to evade and move around.", //9
		// 						"Next, I will show you how to draw a bow from your back.", //10
		// 						"Move your controller behind your neck.", //11
		// 						"When it vibrate, press the trigger button.", //12
		// 						"Now shoot at the capsule-shaped monster. Put arrow at the bow, hold trigger and pull back.", //13
		// 						"Release to shoot, now shoot at the capsule-shaped monsters.", //14
		// 						"Good job, you can choose skills by touching the left touch pad.", //15
		// 						"Skilled-arrow must be fully charged to shoot", //16
		// 						"They have cooldown time. Be aware of it.", //17
		// 						"Let’s try another weapon, the legendary sword.", //18
		// 						"Press the trigger button at your back again.", //19
		// 						"The sword can imbue with the magic crystals to gain special power.", //20
		// 						"Use touch pad to select crystal and put in your sword.", //21
		// 						"The red crystal give the magic power absorption skill when hit.", //22
		// 						"Use it to heal yourself.", //23
		// 						"The blue crystal has great magic power.", //24
		// 						"Diffuse it with the sword then it will become magic greatsword.", //25
		// 						"A hit with the enchanted sword will deplete all the crystal power.", //26
		// 						"Crystals have the cooldown time to recover its power, use it wisely.", //27
		// 						"I hope this is the last time seeing you.", //28
		// 						"Defeat you own creation.", //29
		// 						"Bring peace to this land and you can rest in peace.", //30
		// 						"GoodLuck."}; //31

		isEndTutor = false;
		isTutor = false;
		isUpdateText = false;
		isHitTarget = false;

		color = this.GetComponent<Renderer>().material.GetColor("_Color");

		canvasAnimator = canvas.GetComponent<Animator>();
		textAnimator = text.GetComponent<Animator>();

		// NextTalkScript();
		tutorDashState.StartState();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveUpDown();
		LookAtPlayer();

		currentState.UpdateState();

		// FadeOut();
	}
	// void Update () {
	// 	MoveUpDown();
	// 	LookAtPlayer();

		// AutoTutorial();
		// Tutorial();

		// FadeOut();
		// Debug.Log(counter + " next: " + nextScriptIndex);
		// Debug.Log(isEndTutor);
	// }

	void CurrentState() {
		
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
						// dashTarget.SetActive(true);
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
			Debug.Log("EndTutor");
			isEndTutor = true;
		}

		if(!isEndTutor){
			// NextTalkScript();
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

	// void NextTalkScript(){
	// 	counter++;
	// 	UpdateText(talkScript[counter]);
	// 	TutorPicControl();
	// 	isUpdateText = false;
	// }

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log(isTutor);
		if (col.tag.Equals("projectile") && !isTutor) {
			Debug.Log("EndTutor");
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

				// windTutorSound.isStartFadeOut = true;
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

	// public void Next () {
	// 	if(counter+1 < talkScript.Length){
	// 		counter++;
	// 		UpdateText(talkScript[counter]);
	// 		TutorPicControl();
	// 	}
	// 	else{
	// 		Debug.Log("EndTutor");
	// 		isEndTutor = true;
	// 	}
	// }

	// public void Back () {
	// 	if(counter-1 > 0){
	// 		counter--;
	// 		UpdateText(talkScript[counter]);
	// 		TutorPicControl();
	// 	}
	// }
}
