using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
	private SceneController sceneCon = new SceneController();
	private Color black = Color.black;
	private Color dieTextColor;


	public Material skyboxChaos;
	public Material skyboxNorm;
	public GameObject normalLight, chaosLight, outerEnvi, tutorEnvi, tutorAI;
	public bool tutorialState, AIOpen, afterAIOpen, mainGame, end, isFallingPlay, isNearFallPlay, isDestroyAI, isPlayEarth, isWaitDie, BGMplay;
	public PlaySound fallingWindSoundPlayer, nearFloorSoundPlayer, earthQuakeSoundPlayer, mainBGM, endSong;
	public GameObject dieSound;
	public GameObject sceneDestroyer, sceneProps;
	public GameObject playerTransFilter;
	public GameObject dieCanvas, dieBG, dieText;
	// Use this for initialization
	private static GameState _instance;
	public static GameState instance
	{
		get
		{
			if ( _instance == null )
			{
				_instance = FindObjectOfType<GameState>();
			}
			return _instance;
		}
	}

	void Start () {
		Debug.Log("Game Start");
		playerTransFilter.SetActive(true);

		tutorialState = true;
		AIOpen = false;
		afterAIOpen = false;
		mainGame = false;
		end = false;
		isFallingPlay = false;
		BGMplay = false;
		isNearFallPlay = false;
		isDestroyAI = false;
		isPlayEarth = false;
		isWaitDie = false;
		Physics.IgnoreCollision(StatePatternAI.instance.body.GetComponent<Collider>(), sceneDestroyer.GetComponent<Collider>());
		
		Physics.IgnoreLayerCollision(10, 9);

		black.a = 0;
		dieTextColor = dieText.GetComponent<Text>().color;
		dieTextColor.a = 0;

		fallingWindSoundPlayer.Play();
	}
	void OnEnable(){
		_instance = this;
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
			if(!BGMplay){
				PlayMainBGM();
				BGMplay = true;
			}
			AIOpening();
		}
		if(afterAIOpen) {
			AfterAIOpening();
		}

		if(mainGame) {
			RotateSkyBox();
			// Debug.Log(Player.instance.GetComponent<PlayerStat>().health);
//			if(StatePatternAI.instance.health <= 0){
//				Debug.Log("Purify");
//				mainGame = false;
//				if(BGMplay){
//					StopMainBGM();
//					BGMplay = false;
//				}
//				// end = true;
//			}
			if(Player.instance.GetComponent<PlayerStat>().health <= 0){
				// Debug.Log("You Die");
				StatePatternAI.instance.stopState.StartState();
				// mainGame = false;
				if (!isWaitDie) {
					dieCanvas.SetActive(true);
					StopMainBGM();
					isWaitDie = true;
					dieSound.SetActive(true);
					StartCoroutine(WaitDie());
				}
				black.a += 0.005f;
				dieTextColor.a += 0.005f;

				dieBG.GetComponent<Image>().color = black;
				dieText.GetComponent<Text>().color = dieTextColor;
			}
		}

		if(end) {
			// if(BGMplay){
			// 	StopMainBGM();
			// 	BGMplay = false;
			// }
			Debug.Log("Purify");
			mainGame = false;
			if(BGMplay){
				StopMainBGM();
				BGMplay = false;
			}
			Debug.Log("End");
			if(!isDestroyAI){
				RenderSettings.ambientMode = AmbientMode.Skybox;
				RenderSettings.ambientIntensity = 5f;
				skyboxNorm.SetFloat("_Exposure", 8f);
				Player.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				Player.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
				Player.instance.GetComponent<Rigidbody>().useGravity = false;
				Player.instance.GetComponent<Rigidbody>().isKinematic = true;
				RenderSettings.skybox = skyboxNorm;
				normalLight.SetActive(true);
				chaosLight.SetActive(false);
				Destroy(StatePatternAI.instance.gameObject);
				outerEnvi.SetActive(true);
				isDestroyAI = true;
				endSong.Play();
			}

			if(skyboxNorm.GetFloat("_Exposure") > 1.3f){
				skyboxNorm.SetFloat("_Exposure", skyboxNorm.GetFloat("_Exposure") - 0.1f);
				RenderSettings.ambientIntensity -= 0.055f;
			}

			if(Player.instance.transform.position.y < 500){
				Player.instance.transform.position = Player.instance.transform.position + Vector3.up * 0.05f;
			}
			else {
				sceneCon.ChangeScene(SceneController.MAIN_MENU);
			}
		}
	}

	IEnumerator WaitDie(){
		yield return new WaitForSeconds(10f);
		sceneCon.ChangeScene(SceneController.MAIN_MENU);
	}
	void PlayMainBGM(){
		mainBGM.Play();
		Debug.LogWarning("Main bgm is playing");
	}
	void StopMainBGM(){
		mainBGM.Stop();
	}
	void TutorialState(){
		// Debug.Log(tutorAI.GetComponent<TutorialAI>().isEndTutor);
		// if(tutorAI.GetComponent<TutorialAI>().isEndTutor && !isFallingPlay){
		// 	Debug.Log("Destryo tutorenvi");
		// 	Destroy(tutorEnvi.gameObject);
		// 	fallingWindSoundPlayer.Play();
		// 	isFallingPlay = true;
		// }
		// fallingWindSoundPlayer.Play();
		// isFallingPlay = true;

		if(Player.instance.transform.position.y <= 150f && !isNearFallPlay){
			nearFloorSoundPlayer.Play();
			fallingWindSoundPlayer.isStartFadeOut = true;
			isNearFallPlay = true;
		}

		if(Player.instance.GetComponent<PlayerControl>().isOnFloor == true){
			if(!isPlayEarth){
				earthQuakeSoundPlayer.Play();
				isPlayEarth = true;
			}
			Player.instance.GetComponent<Rigidbody>().isKinematic = true;
			Player.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
			
			tutorialState = false;
			Player.instance.GetComponent<Rigidbody>().isKinematic = false;
			AIOpen = true;

			outerEnvi.SetActive(false);
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
			earthQuakeSoundPlayer.Stop();
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
