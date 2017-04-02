using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController{
	public const string MAIN_MENU = "";
	public const string TUTORIAL = "";
	public const string GAME = "";
	public const string DEAD = "";

	public void ChangeScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
