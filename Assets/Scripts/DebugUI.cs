using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DebugUI : MonoBehaviour {

	// Use this for initialization
	private GameObject playerStat;
	private Text staText;

	void Start () {
		staText = this.transform.Find("Text").GetComponent<Text>();
		playerStat = Player.instance.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		updateStaText();
	}

	void updateStaText() {
		staText.text = "Stamina: " + playerStat.GetComponent<PlayerStat>().stamina +
					   "\nHealth: " + playerStat.GetComponent<PlayerStat>().health;
	}
}
