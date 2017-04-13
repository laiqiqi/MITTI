using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObserver : MonoBehaviour {

	// Use this for initialization
	public abstract void OnNowEnable();
	public abstract void OnNowDisable();
	public abstract void OnSkillStart();
	public abstract void OnSkillEnd();
	public abstract void OnObjectDetached();
	public abstract void AddObjectInstance(GameObject a);
	public abstract void SkillChargeUpdate(string a);
	public abstract void SkillOnCooldown();
	public abstract void SkillOffCooldown();
	public abstract void UltimateChargeUpdate(string a);
	public abstract void UltimateOnCooldown();
	public abstract void UltimateOffCooldown();

}
