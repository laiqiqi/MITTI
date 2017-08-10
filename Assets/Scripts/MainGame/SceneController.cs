using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController{
	public const string MAIN_MENU = "StartScene";
	public const string DASH_TUTOR = "DashTutorial";
	public const string ARROW_TUTOR = "ArrowTutorial";
	public const string SWORD_TUTOR = "SwordTutorial";
	public const string GAME = "MainGame";

	public void ChangeScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}

	public string GetCurrentScene(){
		return SceneManager.GetActiveScene().name;
	}
}
