using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Valve.VR.InteractionSystem
{
	public class WeaponStatus : MonoBehaviour {

		// Use this for initialization
		public BarCharge swordCharge; 
		public BarCharge arrowCharge;
		public SkillCharge swordSkill;
		public SkillCharge arrowSkill;
		public Inventory Inventory;

		private GameObject currentWeapon;
		private BarCharge currentCharge;
		private SkillCharge currentSkill;
		void Start () {
			currentWeapon = null;
			currentCharge = null;
			currentSkill = null;
			Inventory.WeaponChanged += new WeaponChangeHandler(WeaponChangeStartCharge);
		}
		void WeaponChangeStartCharge(GameObject newWeapon, Canvas newUI){
			
			//if swordCharge or if bowCharge or if hand is empty
			if(newWeapon != null){
				if(newWeapon.GetComponent<ArrowHand>() != null){
					GameObject previousWeapon = currentWeapon;
					BarCharge previousCharge = currentCharge;
					if(previousCharge != null)
					{
						previousCharge.StopIncrease();
					}
					currentSkill = arrowSkill;
					currentCharge = arrowCharge;
					currentWeapon = newWeapon;
					arrowSkill.SetCanvas(newUI);
					arrowSkill.CheckCoolDown();
					arrowSkill.SetCanvasStatus(true);
					//set swordskill canvas status to false
					arrowCharge.SetCanvas(newUI);
					arrowCharge.StartIncrease();
					newWeapon.GetComponent<ArrowHand>().SkillFired += new SkillFireHandler(SkillFiredListener);
					newWeapon.GetComponent<ArrowHand>().UltimateFired += new UltimateFireHandler(UltimateFiredListener);
					newWeapon.GetComponent<ArrowHand>().DamageDealt += new DamageHandler(DamageDealtListener);
					
				}else if(newWeapon.GetComponent<Sword>() != null){
					GameObject previousWeapon = currentWeapon;
					BarCharge previousCharge = currentCharge;
					if(previousCharge != null)
					{
						previousCharge.StopIncrease();
					}
					//set swordskillcanvas status to true
					arrowSkill.SetCanvasStatus(false);
					currentSkill = swordSkill;
					currentCharge = swordCharge;
					// swordCharge.SetCanvas(newUI);
					// swordCharge.StartIncrease();
					currentWeapon = newWeapon;
				}
			}
			else{
				//stop currentWeaponPassiveRate
				BarCharge previousCharge = currentCharge;
				if(previousCharge != null)
				{
					previousCharge.StopIncrease();
				}
				currentSkill = null;
				currentWeapon = null;
				currentCharge = null;
				arrowSkill.SetCanvasStatus(false);
				//set swordskill canvas status to false
			}
		}
		private void UltimateFiredListener(){
			currentCharge.Reset();
		}
		private void SkillFiredListener(Skill skill){
			Debug.Log(currentSkill);
			Debug.Log("skill" + skill);
			currentSkill.InitiateCD(skill.GetCoolDown());
		}

		private void DamageDealtListener(){
			currentCharge.FlatIncrease();
		}
		// Update is called once per frame
		void Update () {
			
		}

	}
}