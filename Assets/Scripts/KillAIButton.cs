using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KillAIButton : MonoBehaviour{
	public GameObject tutorialAI;
	public void KillAI() {
		StatePatternAI.instance.health = 100;
	}

	public void SkipTutorial() {
		Debug.Log("EndTutor");
		tutorialAI.GetComponent<TutorialAI>().isEndTutor = true;
	}

	public void KillPlayer() {
		Player.instance.GetComponent<PlayerStat>().health = 0;
	}
}
