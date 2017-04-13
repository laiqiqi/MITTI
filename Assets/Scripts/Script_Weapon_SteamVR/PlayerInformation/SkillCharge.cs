using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCharge : MonoBehaviour {
	Canvas UI;
	SkillObserver UIob;
	bool cooldownfinish = true;
	bool canvasStatus = true;
	int bar;
	public void CheckCoolDown(){
		if(cooldownfinish){
			UIob.SkillOffCooldown();
		}else{
			UIob.SkillOnCooldown();
		}
	}

	public void SetCanvasStatus(bool f){
		canvasStatus = f;
	}

	public void InitiateCD(int cd){
		UIob.SkillOnCooldown();
		cooldownfinish = false;
		StartCoroutine(InitiateCountDown(cd));
	}
	private void updateText(){
		UIob.SkillChargeUpdate(bar.ToString());
	}
	public void SetCanvas(Canvas newUI){
		UI = newUI;
		UIob = UI.GetComponent<SkillObserver>();
	}
	IEnumerator InitiateCountDown(int CoolDown){
			//get the debug text
			//substract -1 from the cooldown
			//if cooldown is 0 its a bug 
			Debug.Log("Cooldown insta");
			bar = CoolDown;
			//if cooldown is 0
			while(bar != 0){
				bar--;
				// coolDownText[CurrentSlot].text = currentTime.ToString();
				if(canvasStatus){
					updateText();
				}
				if(bar == 0){
				// 	Debug.Log("cooldown completed");
					cooldownfinish = true;
					if(canvasStatus){
					UIob.SkillOffCooldown();
					}
				}
				yield return new WaitForSeconds(1f);
			}
		}

}
