using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugUI : MonoBehaviour {

	// Use this for initialization
	public GameObject playerStat;
	private Text staText;

	void Start () {
		staText = this.transform.Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		updateStaText();
	}

	void updateStaText() {
		staText.text = "Stamina:" + playerStat.GetComponent<PlayerStat>().stamina;
	}
}
