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
	public GameObject leftPic, rightPic;

	private string[] talkScript;
	private int counter, nextScriptIndex;
	private Animator canvasAnimator, textAnimator;
	private Color color;

	public int hitCounter;
	public Sprite[] viveButtons;

	public GameObject queryChan;
	public GameObject[] dashTarget;

	// public GameObject gameCon;

	[HideInInspector] public AIState currentState;
	[HideInInspector] public TutorDashState tutorDashState;
	// [HideInInspector] public TutorArrowState tutorArrowState;

	public int dashCount;

	// Use this for initialization
	void Start () {
		tutorDashState = new TutorDashState(this);
		// tutorArrowState = new TutorArrowState(this);

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

	public void SetTutorPicControl(Sprite left, Sprite right) {
		leftPic.GetComponent<SpriteRenderer>().sprite = left;
		rightPic.GetComponent<SpriteRenderer>().sprite = right;
	}

	void MoveUpDown() {
		transform.position += Vector3.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log(isTutor);
		// if (col.tag.Equals("projectile") && !isTutor) {
		if (col.tag.Equals("projectile")) {
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
