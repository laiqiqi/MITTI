using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour {
	public GameObject HPBar;
	public GameObject StaminaBar;
//	public GameObject Player;
	// Use this for initialization
	void Start () {
//		HPBar = Player.instance.GetComponent<PlayerStat> ().health;
//		StaminaBar = Player.instance.GetComponent<PlayerStat> ().stamina;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = this.transform.position;
		newPos.y = Player.instance.transform.position.y;
		this.transform.position = newPos;
		Vector3 pos1 = Player.instance.headCollider.transform.position + Player.instance.headCollider.transform.forward;
		pos1.y = 0;
		Vector3 pos2 = this.transform.position;
		pos2.y = 0;
		Vector3 relativePos = pos1 - pos2;
		this.transform.rotation = Quaternion.LookRotation (relativePos);
		PlayerStat ps = Player.instance.GetComponent<PlayerStat> ();
		HPBar.gameObject.GetComponent<Image> ().fillAmount = ps.health / ps.maxHealth * 0.5f;
		StaminaBar.gameObject.GetComponent<Image> ().fillAmount = ps.stamina/100f*0.5f;

	}
}
