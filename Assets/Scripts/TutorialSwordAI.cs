using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class TutorialSwordAI : MonoBehaviour {

	public float frequency, magnitude;
	public bool isEndTutor, isTutor, isUpdateText, isHitTarget, isCreateTarget;
	public GameObject canvas;
	public Text text;
	public PlaySound canvasSoundPlayer, windTutorSound, tutorBGM;
	private GameObject[] minions;
	public GameObject minion;
	public Transform SpawnPt;
	private bool spawn = false;
	public GameObject nextArrow, backArrow;
	public GameObject frontPic, rightPic;
	private int debugMinNum = 0;
	public Inventory inventory;
	private bool weaponChange = false;
	private bool orbActivated = false;
	private bool skillAttached = false;

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;
	public int hitCounter;

	private bool minionHit = false;
	public Sprite[] viveButtons;

	public GameObject queryChan;
	public GameObject dashTarget;

	// Use this for initialization
	void Start () {
		Player.instance.GetComponent<PlayerControl>().isDashable = true;
		inventory.WeaponNotify += WeaponChangeHandler;
		minions = new GameObject[SpawnPt.childCount];
		text.material.SetColor("_Color", Color.white);
		counter = -1;
		nextScriptIndex = counter+1;

		talkScript = new string[]{"Take out your sword", //0
								"Good, now test them on the little fuckers.", //1
								"The sword skills can be activated with orbs",
								"Touch the touchpad on the other controller",
								"You have completed the basics, go forth bitch"}; //26

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
		DebugMinion();
	}

	void WeaponChangeHandler(){
		weaponChange = true;
	}

	void OrbActivatedHandler(){
		orbActivated = true;
	}

	void AuraCombinedHandler(){
		skillAttached = true;
	}

	public void OnMinionHit(){
		minionHit = true;
	}

	void DebugMinion(){
		if(Input.GetKeyDown(KeyCode.X) || minionHit){
			if(debugMinNum < minions.Length){
			debugMinNum++;

			}
			minionHit = false;
		}
	}
	
	void Tutorial(){
		// Tutorial Start!!!
		if(counter < talkScript.Length && !isEndTutor){
			if (!isUpdateText) {
				// Debug.Log("isTutor "+isTutor);
				if (counter == 0){
					if(weaponChange){
						if(inventory.GetSpawnedItem().GetComponent<Sword>() == null){
							UpdateText("Change your weapon to the sword!");
						}else{
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraChange += OrbActivatedHandler;
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraCombined += AuraCombinedHandler;
							StartCoroutine(CountDown(.1f));
						}
						weaponChange = false;
					}
				}
				else if (counter == 1) {
					//Wait for down sec to next
					if(!spawn){
						SpawnMinions();
					}
					if(weaponChange){
						if(inventory.GetSpawnedItem().GetComponent<Sword>() == null){
							UpdateText("Change your weapon to the sword!");
						}else{
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraChange += OrbActivatedHandler;
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraCombined += AuraCombinedHandler;
							UpdateText("Good, now try the sword on these targets");
						}
						weaponChange = false;
					}
					//StartCoroutine(CountDown(1f));
					checkMinionDeath();
				}
				else if (counter == 2) {
					StartCoroutine(CountDown(2f));
				}
				else if (counter == 3) {
					if(weaponChange){
						if(inventory.GetSpawnedItem().GetComponent<Sword>() == null){
							UpdateText("Change your weapon to the sword!");
						}else{
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraChange += OrbActivatedHandler;
							inventory.GetOtherHandItem().GetComponent<AuraHand>().AuraCombined += AuraCombinedHandler;
							UpdateText("Touch the touch pad on the other controller");
						}
						weaponChange = false;
					}
					
					if(orbActivated){
						UpdateText("Now place it in the socket of the sword");
					}
					orbActivated = false;

					if(skillAttached){
						UpdateText($"You have activated the a sword skill!");
						StartCoroutine(CountDown(2.5f));
					}
					skillAttached = false;	
				}
				else if(counter == 4){
					StartCoroutine(CountDown(2.5f));
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
			Debug.Log("EndTutor");
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
	void SpawnMinions(){
		for(int i=0; i<SpawnPt.childCount; i++){
			minions[i] = Instantiate(minion, SpawnPt.GetChild(i));
			minions[i].transform.localPosition = Vector3.zero;
			

		}
		spawn = true;
	}

	void checkMinionDeath(){
		if(debugMinNum == minions.Length){
			Debug.Log("mininosedeath");
			StartCoroutine(CountDown(0.1f));
		}
	}
	void NextTalkScript(){
		counter++;
		Debug.Log($"Counter {counter}");
		UpdateText(talkScript[counter]);
		TutorPicControl();
		isUpdateText = false;
	}

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		// Debug.Log(isTutor);
		// if (col.tag.Equals("projectile") && !isTutor) {
		// 	Debug.Log("EndTutor");
		// 	isEndTutor = true;
		// }
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
				// foreach (GameObject minion in minions) {
				// 	Destroy(minion);
				// }

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
			Debug.Log("EndTutor");
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
