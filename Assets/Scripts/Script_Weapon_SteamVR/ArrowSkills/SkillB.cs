using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class SkillB : Skill {

		// Use this for initialization
		private FireSource fire;
		private TextMesh debugText;
		public int cooldown;
		public float chargeTime = 0.77f;
		public bool charging = false; //if the arrow is charging
		public bool fullycharged = false; // if the bow is fully charged (guage at 100)
		public bool overcharged = false; // overcharged is having charged state for too long
		private float guage = 0;
		private float maxguage = 100f;
		public float overchargetime = 5f;

		private string skilltype = "normal";

		private IEnumerator increaseGuage;
		public ParticleSystem chargingAura;
		void Start(){
			debugText = GetComponentInChildren<TextMesh>();
			increaseGuage = IncreaseGuage();
			fire = GetComponentInChildren<FireSource>();
		}
		public override void Uncharging(bool OnRelease){
			charging = false;
			chargingAura.Stop();
			if(!OnRelease){
			fire.DestroyFire();
			}
			StopCoroutine(increaseGuage);		
			Debug.Log("skill B: Uncharging");
			CancelInvoke("SetOverCharge");
			// StartCoroutine(decreaseGuage = DecreaseGuage());	
			guage = 0;
			fullycharged = false;
			overcharged = false;
			DebugText(guage);
		}
		public override void Charging(){
			chargingAura.Play();
			charging = true;
			//StopCoroutine(decreaseGuage);
			Debug.Log("skill B: Charging");
			StartCoroutine(increaseGuage = IncreaseGuage());
		}

		public override bool GetChargingStatus(){
			return charging;
		}
		private void InitiateSkillOnCharge(){
			fire.SendMessage( "FireExposure", SendMessageOptions.DontRequireReceiver );

		}
		public override void InitiateSkillOnRelease(){


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
			fire.DestroyFire();
			
			// the arrow still thinks it will initiate skill, make sure to reset that 
		}

		public override void ResetOverCharge(){
			Debug.Log("skill reset overcharge");
			overcharged = false;
		}
		public override bool OverCharged(){return overcharged;}
		// Update is called once per frame

		IEnumerator IncreaseGuage(){
			// Debug.Log("increasing");
			// while(guage < maxguage){
			// 	guage++;
			// 	// Debug.Log(guage);
			// 	if(guage == maxguage){
			// 		fullycharged = true;
			// 		InitiateSkillOnCharge();
			// 		Invoke("SetOverCharge",overchargetime);
			// 	}
			// 	DebugText(guage);
			// 	yield return new WaitForSeconds(0.005f);
			// }
			yield return new WaitForSeconds(chargeTime);
			fullycharged = true;
			InitiateSkillOnCharge();
			chargingAura.Stop();
			Invoke("SetOverCharge",5);
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