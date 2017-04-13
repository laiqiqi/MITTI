using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skill : MonoBehaviour {

	public abstract bool GetChargingStatus();
	// Use this for initialization
	public abstract void Charging();
	public abstract void Uncharging();
	public abstract void InitiateSkillOnRelease();
	public abstract void StopSkill();

	public abstract bool GetFullyCharged();

	public abstract void SetOverCharge();

	public abstract bool OverCharged();

	public abstract void ResetOverCharge();
	public abstract int GetCoolDown();

	public abstract string GetSkillType();
	// Update is called once per frame
}
