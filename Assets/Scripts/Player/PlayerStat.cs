using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

	// Use this for initialization
	public float stamina;
	public float staRegenRate;

	public float health;

//--------------------Player Status--------------------------
	[HideInInspector] public bool isHitSlam, isStartDazzle, isDazzle;
//-----------------------------------------------------------
	void Start () {
		ResetAllStatusToFalse();
		
		stamina = 100;
		staRegenRate = 0.5f;

		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		// StaminaRegen();
		RegenStamina(staRegenRate);
	}

	void RegenStamina(float rate){
		if(stamina < 100){
			if(stamina + (rate) > 100){
				stamina = 100;
			}
			else{
				stamina += (rate);
			}
		}
	}

	public void ResetAllStatusToFalse() {
		isHitSlam = false;
		isStartDazzle = false;
		isDazzle = false;
	}
}
