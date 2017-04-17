using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BloodFilter : MonoBehaviour {

	private PlayerStat playerStat;
	private Color color;
	// Use this for initialization
	void Start () {
		playerStat = Player.instance.GetComponent<PlayerStat>();
		color = this.GetComponent<Image>().material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		color.a = (100 - playerStat.health)/100f;
		this.GetComponent<Image>().material.SetColor("_Color", color);
	}
}
