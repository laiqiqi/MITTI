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
	public bool tutorialState, AIOpen, afterAIOpen, mainGame, end, isFallingPlay, isNearFallPlay;
	public PlaySound fallingWindSoundPlayer, nearFloorSoundPlayer;
	public GameObject sceneDestroyer, sceneProps;
	public GameObject playerTransFilter;

	// Use this for initialization
	void Start () {
		tutorialState = true;
		AIOpen = false;
		afterAIOpen = false;
		mainGame = false;
		end = false;
		isFallingPlay = false;
		Physics.IgnoreCollision(StatePatternAI.instance.body.GetComponent<Collider>(), sceneDestroyer.GetComponent<Collider>());
		Physics.IgnoreLayerCollision(10, 9);
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
			normalLight.SetActive(true);
			chaosLight.SetActive(false);
			Destroy(StatePatternAI.instance.gameObject);
		}
	}

	void TutorialState(){
		if(tutorAI.GetComponent<TutorialAI>().isEndTutor && !isFallingPlay){
			Destroy(tutorEnvi.gameObject);
			fallingWindSoundPlayer.Play();
			isFallingPlay = true;
		}
		if(Player.instance.transform.position.y <= 150f && !isNearFallPlay){
			nearFloorSoundPlayer.Play();
			fallingWindSoundPlayer.isStartFadeOut = true;
			isNearFallPlay = true;
		}
		if(Player.instance.GetComponent<PlayerControl>().isOnFloor == true){
			Player.instance.GetComponent<Rigidbody>().isKinematic = true;
			
			tutorialState = false;
			AIOpen = true;

			Destroy(outerEnvi);
			Destroy(tutorAI);
			
			StatePatternAI.instance.stopState.EndState();
			StatePatternAI.instance.openingState.StartState();
		}
	}

	void AIOpening(){
		if(StatePatternAI.instance.transform.position.y > 4.4f){
			sceneDestroyer.SetActive(true);
		}

		if(StatePatternAI.instance.currentState != StatePatternAI.instance.openingState){
			AIOpen = false;

			normalLight.SetActive(false);
			chaosLight.SetActive(true);
			RenderSettings.skybox = skyboxChaos;
			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.ambientIntensity = 5f;
			skyboxChaos.SetFloat("_Exposure", 8f);
			Debug.Log("force");
			sceneDestroyer.GetComponent<SceneDestroyer>().force = 10f;
			sceneDestroyer.GetComponent<SceneDestroyer>().upwardsModifier = 5f;
			
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
			Destroy(sceneDestroyer);
			Destroy(sceneProps);
		}
	}

	void RotateSkyBox(){
		if(skyboxChaos.GetFloat("_Rotation") >= 360f){
			skyboxChaos.SetFloat("_Rotation", 0f);
		}
		else{
			skyboxChaos.SetFloat("_Rotation", skyboxChaos.GetFloat("_Rotation") + 0.01f);
		}
	}
}
