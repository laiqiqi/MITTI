using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class PlayerSummonBall : MonoBehaviour {

	public GameObject transFilter;
	public PlaySound windSoundPlayer1, windSoundPlayer2, lightSoundPlayer, sceneChangeSound;

	private SceneController sceneCon;
	private Color color;
	private bool isChangeScene, isPlayChangeScene;
	// Use this for initialization
	void Start () {
		sceneCon = new SceneController();
		isChangeScene = false;
		isPlayChangeScene = true;
		color = Color.white;
		color.a = 0f;
		transFilter.GetComponent<Image>().material.SetColor("_Color", color);
	}

	void Update () {
		if(isChangeScene){
			Debug.Log(transFilter.GetComponent<Image>().material.GetColor("_Color").a);
			if(transFilter.GetComponent<Image>().material.GetColor("_Color").a < 1f){
				color.a += 0.05f;
				transFilter.GetComponent<Image>().material.SetColor("_Color", color);
				if(transFilter.GetComponent<Image>().material.GetColor("_Color").a >= 0.5f && isPlayChangeScene){
					sceneChangeSound.Play();
					isPlayChangeScene = false;
				}
			}
			else{
				sceneCon.ChangeScene(SceneController.GAME);
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player"){
			isChangeScene = true;
			transFilter.SetActive(true);
			windSoundPlayer1.isStartFadeOut = true;
			windSoundPlayer2.isStartFadeOut = true;
			lightSoundPlayer.isStartFadeOut = true;
		}
	}
}
