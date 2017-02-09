using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEventListener : MonoBehaviour {
	
	private bool isDashing;
	private GameObject player;
	private GameObject head;
	private Vector3 moveTarget;
	private Rigidbody playerRb;
	private float headPosY;
	private PlayerStat playerStat;

	// Use this for initialization
	void Start () {
		if (GetComponent<VRTK.VRTK_ControllerEvents>() == null)
		{
			Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}

		//Setup controller event listeners
		GetComponent<VRTK.VRTK_ControllerEvents>().TouchpadPressed += new VRTK.ControllerInteractionEventHandler(DoTouchpadPressed);
		GetComponent<VRTK.VRTK_ControllerEvents>().TouchpadReleased += new VRTK.ControllerInteractionEventHandler(DoTouchpadReleased);

		player = transform.parent.gameObject;
		head = player.transform.Find("Camera (eye)").gameObject;
		headPosY = player.transform.GetChild(2).position.y;
		playerRb = player.GetComponent<Rigidbody>();
		playerStat = player.GetComponentInChildren<PlayerStat>();
		// Debug.Log(playerStat.stamina);
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(head.transform.forward);
	}

	private void DoTouchpadPressed (object sender, VRTK.ControllerInteractionEventArgs e) {
		Debug.Log("Press");

	}

	private void DoTouchpadReleased (object sender, VRTK.ControllerInteractionEventArgs e) {
		Debug.Log("Release");
		// moveTarget = new Vector3(player.transform.position.x + (head.transform.forward.x * 15f),
		// 								player.transform.position.y,
		// 								player.transform.position.z + (head.transform.forward.z * 15f));
		// Debug.Log(moveTarget);
		Dash();
	}

	void Dash() {
		if (playerStat.stamina > 50f) {
			Vector3 direction = new Vector3(head.transform.right.x * 9f, 0f, head.transform.right.z * 9f);
			direction = Quaternion.Euler(0, -90, 0) * direction;
			playerRb.AddForce(direction, ForceMode.VelocityChange);
			// playerRb.AddForce(head.transform.forward.x * 9f, 0f, head.transform.forward.z * 9f,  ForceMode.VelocityChange);
			playerStat.stamina -= 50f;
			// player.transform.position = Vector3.MoveTowards(player.transform.position,
			// 											moveTarget, 10f * Time.deltaTime);
		}
	}
}
