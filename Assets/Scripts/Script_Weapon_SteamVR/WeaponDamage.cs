using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{

public delegate void DamageDealtHandler(float dmg);
public class WeaponDamage : MonoBehaviour {

	void Start(){
		
	}
	public event DamageDealtHandler OnDamageDealt;
	// Use this for initialization
	private void DamageDealt(float dmg){
		if(OnDamageDealt != null){
			OnDamageDealt(dmg);
		}
	}
	void HasAppliedDamage(float dmg){
		DamageDealt(dmg);
		Debug.Log("this shit dealt dmg at"+ dmg);
	}
}

}
