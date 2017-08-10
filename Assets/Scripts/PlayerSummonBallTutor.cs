using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class PlayerSummonBallTutor : MonoBehaviour {

	public GameObject transFilter, portalLight, portal;
	public PlaySound lightSoundPlayer, sceneChangeSound;

	private SceneController sceneCon;
	private Color color, gameTitleColor;
	private bool isChangeScene, isPlayChangeScene, isLightUp;
	private float r, g, b;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		sceneCon = new SceneController();
		isChangeScene = false;
		isPlayChangeScene = true;
		color = Color.white;
		color.a = 0f;
		transFilter.GetComponent<Image>().material.SetColor("_Color", color);
		isLightUp = true;
	}

	void Update () {
		if(isChangeScene){
			// Debug.Log(transFilter.GetComponent<Image>().material.GetColor("_Color").a);
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
		
		PortalLightControl();
	}

	void OnTriggerEnter(Collider col) {
		if(col.tag == "Player"){
			transFilter.SetActive(true);
			lightSoundPlayer.isStartFadeOut = true;
			isChangeScene = true;
		}
	}

	void PortalLightControl() {
		if(isLightUp) {
			if(portalLight.GetComponent<Light>().intensity <= 2.5f){
				portalLight.GetComponent<Light>().intensity += 0.05f;
				var emission = portal.GetComponent<ParticleSystem>().emission;
				emission.rateOverTime = 200f;
				r += 0.05f;
				g += 0.05f;
				b += 0.05f;
				Color color = new Color(r, g, b, 1);
			}
			else{
				isLightUp = !isLightUp;
			}
		}
		else if(!isLightUp){
			if(portalLight.GetComponent<Light>().intensity >= 2.25f){
				portalLight.GetComponent<Light>().intensity -= 0.05f;
				var emission = portal.GetComponent<ParticleSystem>().emission;
				emission.rateOverTime = 100f;
				r -= 0.05f;
				g -= 0.05f;
				b -= 0.05f;
				Color color = new Color(r, g, b, 1);
			}
			else{
				isLightUp = !isLightUp;
			}
		} 
	}
}
