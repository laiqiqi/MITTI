using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAIButton : MonoBehaviour{
	public GameObject tutorialAI;
	public void KillAI() {
		StatePatternAI.instance.health = 0;
	}

	public void SkipTutorial() {
		tutorialAI.GetComponent<TutorialAI>().isEndTutor = true;
	}
}
