using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

	// Use this for initialization
	public float stamina;
	public float staRegenRate, healthRegenRate;

	public float  maxHealth;
	[HideInInspector] public float health;

//--------------------Player Status--------------------------
	[HideInInspector] public bool isHitSlam, isStartDazzle, isDazzle, isTakeDamage, isHeal;
//-----------------------------------------------------------
	void Start () {
		ResetAllStatusToFalse();
		health = maxHealth;
		
		stamina = 100;
		staRegenRate = 0.5f;

	
		healthRegenRate = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		// StaminaRegen();
		// Debug.Log("Player y pos: " + transform.position.y);
		RegenStamina(staRegenRate);
		RegenHealth(healthRegenRate);
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

	void RegenHealth(float rate){
		if(health < 100){
			if(health + (rate) > 100){
				health = 100;
			}
			else{
				health += (rate);
			}
		}
	}

	public void ResetAllStatusToFalse() {
		isHitSlam = false;
		isStartDazzle = false;
		isDazzle = false;
	}

	public void PlayerTakeDamage(float dmg) {
		isTakeDamage = true;
		if(health - dmg < 0){
			health = 0;
		}
		else{
			health -= dmg;
		}
	}

	public void PlayerHeal(float heal){
		isHeal = true;
		if(health + heal >= maxHealth){
			health = maxHealth;
		}
		else{
			health += heal;
		}
	}


	
}
