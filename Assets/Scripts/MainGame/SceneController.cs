using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController{
	public const string MAIN_MENU = "StartScene";
	public const string TUTOR_PART1 = "TutorialPart1";
	public const string GAME = "MainGameWithTutorial";
	public const string DEAD = "";

	public void ChangeScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
