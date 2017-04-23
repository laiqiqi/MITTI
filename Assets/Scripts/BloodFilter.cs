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
		if (Player.instance.GetComponent<PlayerStat>().isTakeDamage) {
			color.a = 1;
			this.GetComponent<Image>().material.SetColor("_Color", color);
			Player.instance.GetComponent<PlayerStat>().isTakeDamage = false;
		}
		else {
			if(this.GetComponent<Image>().material.GetColor("_Color").a > 0){
				color.a -= 0.2f;
				this.GetComponent<Image>().material.SetColor("_Color", color);
			}
		}
	}
}
