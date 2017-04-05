using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSummonBall : MonoBehaviour {

	public GameObject transFilter;

	private SceneController sceneCon;
	private Color color;
	private bool isChangeScene;
	// Use this for initialization
	void Start () {
		sceneCon = new SceneController();
		isChangeScene = false;
		color = Color.white;
		color.a = 0f;
		transFilter.GetComponent<Renderer>().material.SetColor("_Color", color);
	}

	void Update () {
		if(isChangeScene){
			Debug.Log(transFilter.GetComponent<Renderer>().material.GetColor("_Color").a);
			if(transFilter.GetComponent<Renderer>().material.GetColor("_Color").a < 1f){
				color.a += 0.005f;
				transFilter.GetComponent<Renderer>().material.SetColor("_Color", color);
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
		}
	}
}
