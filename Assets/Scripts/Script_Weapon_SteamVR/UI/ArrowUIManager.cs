﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Valve.VR.InteractionSystem
{
	public class ArrowUIManager : SkillObserver {
		private GameObject itemPackageReference;	
		private ArrowHand arrowHand;
		private Hand hand;
		private Vector2 fingerPos;
		private bool[] coolDownStatus;
		private Text[] coolDownText;
		private Image[] UIImages;
		private int cPos;
		private bool colorChange = false;
		private bool Enabled = true;
		// Use this for initialization
		void Awake () {
			//refractor this part: make a cooldown object...its much easier
			UIImages = new Image[3];
			coolDownStatus = new bool[3];
			coolDownText = new Text[3];
			//this for loop goes through the existing icons, which are for ultimate and skill
			//there are 2 of them and current manually match by number
			for(int i = 0; i < transform.childCount; i++){
				GameObject child = transform.GetChild(i).gameObject;
				child.SetActive(false);
				coolDownText[i] = child.transform.Find("CountDown").GetComponent<Text>();
				UIImages[i] = child.GetComponent<Image>();
			}
			UltimateOnCooldown();
		}

		void Start () {	
			Hand otherhand = GetComponentInParent<Hand>().otherHand;
			this.transform.position = otherhand.transform.position;
			this.transform.parent = otherhand.transform;
		}
		void Update(){
			if (hand){
				if(Enabled){
					OnTouchPadEnter(hand);
					OnTouchPadStay(hand);
					OnTouchUp(hand);
				}
			}
		}

		public override void AddObjectInstance(GameObject addedObject){
			if (addedObject.GetComponent<ArrowHand>() != null){
				arrowHand = addedObject.GetComponent<ArrowHand>();
				//add observer to the object
				arrowHand.AddObserver(this);
			}
		}
		public override void OnSkillStart(){
			arrowHand.ChangeSkill(0);
		}
		public override void SkillChargeUpdate(string number){
			coolDownText[1].text = number;
		}

		public override void SkillOnCooldown(){
			coolDownStatus[1] = true;
			coolDownText[1].gameObject.SetActive(true);
			UIImages[1].color = Color.gray;
		}
		public override void SkillOffCooldown(){
			coolDownStatus[1] = false;
			coolDownText[1].gameObject.SetActive(false);
			UIImages[1].color = Color.white;
		}
		public override void UltimateChargeUpdate(string number){
			coolDownText[2].text = number;
			
			// Debug.Log("text update " + coolDownText[2].text);
		}

		public override void UltimateOnCooldown(){
			coolDownStatus[2] = true;
			coolDownText[2].gameObject.SetActive(true);
			UIImages[2].color = Color.gray;
		}
		public override void UltimateOffCooldown(){
			coolDownStatus[2] = false;
			coolDownText[2].gameObject.SetActive(false);
			UIImages[2].color = Color.white;
		}
		private void SetOnCoolDownProperties(){

		}

		private void ResetOnCoolDownProperties(){

		}
		public override void OnSkillEnd(){
			Debug.Log("skill ended");
		}
		public override void OnNowEnable(){
			// Debug.Log("enable");
			EnableUI();
		}

		public override void OnNowDisable(){
			DisableUI();
			
		}
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand.otherHand;
		}

		private void OnUISet(Hand hand){
			OnAttachedToHand(hand); 
		}

		public override void OnObjectDetached(){
			Debug.Log("on arrow ui manager destroy"+gameObject);
			//remove ArrowUIManager from ArrowHand observer list
			arrowHand.RemoveObserver(this);
			Destroy(gameObject);
		}
		void OnTouchPadEnter(Hand hand){
			if (hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad)){
				Show();
				fingerPos = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
				cPos = getTouchQuad(fingerPos);

			}
		}
		void OnTouchPadStay(Hand hand){
			if(hand.controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad ) ){			
				fingerPos = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
				//check change color, if not change
				int touchPos = getTouchQuad(fingerPos);
				int checkedPos = checkOnCoolDown(touchPos);
				if(cPos != touchPos){
					if(checkedPos != -1){
						UIImages[touchPos].color = Color.yellow;
					}
					if(checkOnCoolDown(cPos) != -1){
						UIImages[cPos].color = Color.white;
					}
					cPos = touchPos;
				}

				
				
				updateSelectedSkill(checkOnCoolDown(getTouchQuad(fingerPos)));

			}
		}

		void OnTouchUp(Hand hand){
			if( hand.controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad )){
				Hide();
				int skillPos = checkOnCoolDown(getTouchQuad(fingerPos));
				arrowHand.ChangeSkill(skillPos);
				fingerPos = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
				//if color change, changeback,set change color tofalse
				//play animation
				//spawnAndequip selected weapon
				//clear canvas, stop animation
				if(checkOnCoolDown(cPos)!= -1){
					UIImages[cPos].color = Color.white;
				}
			}
		}
		void updateSelectedSkill(int skillPos){
			if(skillPos != -1){
				UIImages[skillPos].color = Color.yellow;
				// Debug.Log(UIImages[skillPos].color == Color.yellow);
			}
		}
		void Show(){
			for(int i = 0; i < transform.childCount; i++){
				transform.GetChild(i).gameObject.SetActive(true);
			}
		}

		void Hide(){
			for(int i = 0; i < transform.childCount; i++){
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		void DisableUI(){
			Enabled = false;
			Hide();
		}
		
		void EnableUI(){
			Enabled = true;
			// Show();
		}
		int getTouchQuad(Vector2 tPos){
			//condition has to be dynamic to inventory space
			// if the corresponding return values are in state false, then return -1
			if(tPos.x >= -1 && tPos.x <= 0 && tPos.y <= 1 && tPos.y >= 0){
				return 1;
			}else if(tPos.x > 0 && tPos.x <= 1 && tPos.y <= 1 && tPos.y >= 0){
				return 2;
			}else{
				return 0;
			}	
		}

		int checkOnCoolDown(int Pos){
			if(coolDownStatus[Pos] == false){
				return Pos;
			}
			else{
				return -1;
			}
		}
	}
}
