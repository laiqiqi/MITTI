using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AIEffectManager : MonoBehaviour {

	//For enable/disable effects
	public GameObject[] magicCircles;
	public Dictionary<string, GameObject> magicCirclesDict = new Dictionary<string, GameObject>();
	public Dictionary<string, GameObject> tempCircles  = new Dictionary<string, GameObject>();

	public GameObject[] skillEffects;
	public Dictionary<string, GameObject> skillEffectsDict = new Dictionary<string, GameObject>();
	public Dictionary<string, GameObject> tempEffects = new Dictionary<string, GameObject>();

	public PlaySound[] sounds;
	public Dictionary<string, PlaySound> soundsDict = new Dictionary<string, PlaySound>();

	// Use this for initialization
	void Awake () {
		for(int i=0; i<magicCircles.Length; i++) {
			magicCirclesDict.Add(magicCircles[i].name, magicCircles[i]);
		}

		for(int i=0; i<skillEffects.Length; i++) {
			skillEffectsDict.Add(skillEffects[i].name, skillEffects[i]);
		}

		for(int i=0; i<sounds.Length; i++ ){
			soundsDict.Add(sounds[i].name, sounds[i]);
		}
	}

	void Start () {
		
	}
//-----------MagicCircles----------------------------------
	public void CreateCircleByName (string name ,Vector3 effectPos) {
		GameObject circle = (GameObject)Instantiate(magicCirclesDict[name], effectPos, Quaternion.identity);
		tempCircles.Add(name, circle);
	}
	public GameObject CreateAndReturnCircleByName (string name, Vector3 effectPos) {
		GameObject effect = (GameObject)Instantiate(magicCirclesDict[name], effectPos, Quaternion.identity);
		tempCircles.Add(name, effect);
		return tempCircles[name];
	}
	public void DestroyCircleByName (string name) {
		Destroy(tempCircles[name]);
	}

	public void RemoveCircleFromDictByName (string name){
		tempCircles.Remove(name);
	}
//---------------------------------------------------------
//-----------SkillEffect-----------------------------------
	public void CreateEffectByName(string name, Vector3 effectPos) {
		GameObject effect = (GameObject)Instantiate(skillEffectsDict[name], effectPos, Quaternion.identity);
		tempEffects.Add(name, effect);
	}
	public GameObject CreateAndReturnEffectByName (string name, Vector3 effectPos) {
		GameObject effect = (GameObject)Instantiate(skillEffectsDict[name], effectPos, Quaternion.identity);
		tempEffects.Add(name, effect);
		return tempEffects[name];
	}
	public void DestroyEffectByName (string name) {
		Destroy(tempEffects[name]);
	}

	public void RemoveEffectFromDictByName (string name) {
		tempEffects.Remove(name);
	}
//---------------------------------------------------------
//-----------SoundEffect-----------------------------------
	public void PlaySoundByName (string name) {
		soundsDict[name].Play();
	}

	public void StopSoundByName (string name) {
		soundsDict[name].Stop();
	}
//---------------------------------------------------------
}
