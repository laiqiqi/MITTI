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
	[HideInInspector] public TutorArrowState tutorArrowState;

	// Use this for initialization
	void Start () {
		tutorDashState = new TutorDashState(this);
		tutorArrowState = new TutorArrowState(this);

		text.material.SetColor("_Color", Color.white);
		
		isEndTutor = false;
		isTutor = false;
		isUpdateText = false;
		isHitTarget = false;

		color = this.GetComponent<Renderer>().material.GetColor("_Color");

		canvasAnimator = canvas.GetComponent<Animator>();
		textAnimator = text.GetComponent<Animator>();

		tutorDashState.StartState();
		// tutorArrowState.StartState();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveUpDown();
		LookAtPlayer();

		currentState.UpdateState();

		// FadeOut();
	}

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
}
