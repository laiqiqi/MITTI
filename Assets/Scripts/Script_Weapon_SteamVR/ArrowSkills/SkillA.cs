using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class SkillA : Skill {

		// Use this for initialization
		private TextMesh debugText;
		public int cooldown;
		public bool charging = false; //if the arrow is charging
		public bool fullycharged = false; // if the bow is fully charged (guage at 100)
		public bool overcharged = false; // overcharged is having charged state for too long
		private float guage = 0;
		private float maxguage = 100f;

		private string skilltype = "ultimate";

		private IEnumerator increaseGuage;

		void Start(){
			debugText = GetComponentInChildren<TextMesh>();
			increaseGuage = IncreaseGuage();
		}
		public override void Uncharging(){
			charging = false;
			StopCoroutine(increaseGuage);		
			Debug.Log("skill A: Uncharging");
			CancelInvoke("SetOverCharge");
			// StartCoroutine(decreaseGuage = DecreaseGuage());	
			guage = 0;
			fullycharged = false;
			overcharged = false;
			DebugText(guage);
		}
		public override void Charging(){
			charging = true;
			//StopCoroutine(decreaseGuage);
			Debug.Log("skill A: Charging");
			StartCoroutine(increaseGuage = IncreaseGuage());
		}

		public override bool GetChargingStatus(){
			return charging;
		}
		private void InitiateSkillOnCharge(){
			

		}
		public override void InitiateSkillOnRelease(){
			Debug.Log("skill A: Initiating skill");
			if(fullycharged){
				Invoke("activateSkill", 1);
			}
			//call activateChild after 1 sec
			//skill should not be activated right after it fires, so have to wait 1 second
			//in activatechild, destroy current arrow
			//cant destroy arrow, so have to deactivate model and glint
			//child start automatically updating

		}
		public override void StopSkill(){
			CancelInvoke("activateSkill");
			Debug.Log("skill A: stop skill");
			//cancle the activatechild method
		}

		public override bool GetFullyCharged(){return fullycharged;}
		
		public override void SetOverCharge(){
			//fully charged to false
			//uncharge it
			charging = false;
			Debug.Log("SetOverCharge");
			guage = 0;
			fullycharged = false;
			overcharged = true;
			DebugText(guage);
			
			// the arrow still thinks it will initiate skill, make sure to reset that 
		}

		public override void ResetOverCharge(){
			overcharged = false;
		}
		public override bool OverCharged(){return overcharged;}
		// Update is called once per frame

		IEnumerator IncreaseGuage(){
			Debug.Log("increasing");
			while(guage < maxguage){
				guage++;
				// Debug.Log(guage);
				if(guage == maxguage){
					fullycharged = true;
					Invoke("SetOverCharge",3);
				}
				DebugText(guage);
				yield return new WaitForSeconds(0.005f);
			}
		}
		private void activateSkill(){
			//Deactivat everything inside arrow
			GetComponent<BoxCollider>().enabled = false;
			for(int i = 0; i < transform.childCount; i++){
				Transform child = transform.GetChild(i);
				if(transform.GetChild(i).name != "ArrowExplode"){
					child.gameObject.SetActive(false);
				}else{
					child.gameObject.SetActive(true);
				}
			}
		}
		public override int GetCoolDown(){return cooldown;}
		void DebugText(float guage){
			debugText.text = guage.ToString();
		}

		public override string GetSkillType(){
			return skilltype;
		}
	}
}