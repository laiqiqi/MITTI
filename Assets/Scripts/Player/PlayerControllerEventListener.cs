// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using VRTK;

// public class PlayerControllerEventListener : MonoBehaviour {
	
// 	private VRTK_ControllerEvents controllerEvents;

// 	private bool isDashing;
// 	public GameObject player;
// 	private GameObject head;
// 	private Vector3 moveTarget;
// 	private Rigidbody playerRb;
// 	private float headPosY;
// 	private PlayerStat playerStat;

// 	// Use this for initialization
// 	void Start () {
// 		controllerEvents = GetComponent<VRTK_ControllerEvents>();

// 		if (GetComponent<VRTK_ControllerEvents>() == null)
// 		{
//                 Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
//                 return;
//         }

// 		//Setup controller event listeners
// 		GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
// 		GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);

// 		head = player.transform.Find("Camera (eye)").gameObject;
// 		headPosY = player.transform.GetChild(2).position.y;
// 		playerRb = player.GetComponent<Rigidbody>();
// 		playerStat = player.GetComponentInChildren<PlayerStat>();
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 		// Debug.Log(head.transform.forward);
// 	}

// 	private void DoTouchpadPressed (object sender, VRTK.ControllerInteractionEventArgs e) {
// 		Debug.Log("Press");

// 	}

// 	private void DoTouchpadReleased (object sender, VRTK.ControllerInteractionEventArgs e) {
// 		Debug.Log("Release");
// 		Dash();
// 	}

// 	void Dash() {
// 		Debug.Log("dash");
// 		if (playerStat.stamina >= 50f) {
// 			Vector3 direction = new Vector3(head.transform.right.x * 9f, 0f, head.transform.right.z * 9f);
// 			direction = Quaternion.Euler(0, -90, 0) * direction;
// 			playerRb.AddForce(direction, ForceMode.VelocityChange);
// 			// playerStat.stamina -= 50f;
// 		}
// 	}
// }
