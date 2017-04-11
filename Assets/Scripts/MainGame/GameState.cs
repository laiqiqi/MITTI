using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Rendering;

public class GameState : MonoBehaviour {
	private SceneController sceneCon = new SceneController();

	public Material skyboxChaos;
	public Material skyboxNorm;
	public GameObject normalLight, chaosLight, outerEnvi, tutorEnvi, tutorAI;
	public bool tutorialState, AIOpen, afterAIOpen, mainGame, end;

	// Use this for initialization
	void Start () {
		tutorialState = true;
		AIOpen = false;
		afterAIOpen = false;
		mainGame = false;
		end = false;
	}
	
	// Update is called once per frame
	void Update () {
		MainGameState();
	}
	void MainGameState(){
		if(tutorialState) {
			TutorialState();
		}
		if(AIOpen) {
			AIOpening();
		}
		if(afterAIOpen) {
			AfterAIOpening();
		}

		if(mainGame) {
			RotateSkyBox();

			if(StatePatternAI.instance.health <= 0){
				Debug.Log("Purify");
				mainGame = false;
				end = true;
			}
			if(Player.instance.GetComponent<PlayerStat>().health <= 0){
				Debug.Log("You Die");
				mainGame = false;
				sceneCon.ChangeScene(SceneController.DEAD);
			}
		}

		if(end) {
			Debug.Log("End");
			RenderSettings.skybox = skyboxNorm;
		}
	}

	void TutorialState(){
		if(tutorAI.GetComponent<TutorialAI>().isEndTutor){
			Destroy(tutorEnvi.gameObject);
		}
		if(Player.instance.transform.position.y <= 2f){
			tutorialState = false;
			AIOpen = true;
			Destroy(outerEnvi);
			StatePatternAI.instance.stopState.EndState();
			StatePatternAI.instance.awokenState.StartState();
		}
	}

	void AIOpening(){
		if(StatePatternAI.instance.currentState != StatePatternAI.instance.awokenState){
			AIOpen = false;

			normalLight.SetActive(false);
			chaosLight.SetActive(true);
			RenderSettings.skybox = skyboxChaos;
			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.ambientIntensity = 5f;
			skyboxChaos.SetFloat("_Exposure", 8f);
			
			afterAIOpen = true;
		}
	}

	void AfterAIOpening(){
		if(skyboxChaos.GetFloat("_Exposure") > 1f){
			skyboxChaos.SetFloat("_Exposure", skyboxChaos.GetFloat("_Exposure") - 0.1f);
			RenderSettings.ambientIntensity -= 0.055f;
		}
		else{
			afterAIOpen = false;
			mainGame = true;
		}
	}

	void RotateSkyBox(){
		if(skyboxChaos.GetFloat("_Rotation") >= 360f){
			skyboxChaos.SetFloat("_Rotation", 0f);
		}
		else{
			skyboxChaos.SetFloat("_Rotation", skyboxChaos.GetFloat("_Rotation") + 0.1f);
		}
	}
}
