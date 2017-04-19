using System.Collections;
using UnityEngine;

public class BarCharge : MonoBehaviour {
	//the canvas
	// Use this for initialization
	Canvas UI;

	SkillObserver textUpdater;
	int bar;
	int max = 100;
	int increase = 20;

	private IEnumerator passiveIncrease;
	void Start () {
	}

	public void StartIncrease(){
		//check if bar == max
		//the UI manager DOES NOT hold ultimatebar information
		//this is when the ultimate already hits 100, and ready to use, but the weapon changes, the start increase
		//is called after changed of object and just have to tell the UIManager again that it is ready to use.
		if(bar == max){
			textUpdater.UltimateOffCooldown();
		}
		passiveIncrease = PassiveIncrease();
		StartCoroutine(passiveIncrease);
	}
	public void Reset(){
		StopIncrease();
		bar = 0;
		UpdateText();
		textUpdater.UltimateOnCooldown();
		StartIncrease();
	}

	public void FlatIncrease(){
		StopIncrease();
		if(bar+increase < max)
		{
			bar += increase;
		}
		else if(bar < max)
		{
			bar += max - bar;
		}
		StartIncrease();
		
		UpdateText();
	}

	public void StopIncrease(){
		//todo test just for now
		if(passiveIncrease != null){
			StopCoroutine(passiveIncrease);
		}
	}

	public void SetCanvas(Canvas ui){
		UI = ui;
		textUpdater = UI.GetComponent<SkillObserver>();
	}
	private void UpdateText(){
		textUpdater.UltimateChargeUpdate(bar.ToString());
		
	}
	IEnumerator PassiveIncrease() {
		while(bar < max){
			bar++;
				UpdateText();
			if(bar == max){
				textUpdater.UltimateOffCooldown();
			}
			yield return new WaitForSeconds(2f);
		}
	}
}
