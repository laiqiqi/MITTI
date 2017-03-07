using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class PlayerControl : MonoBehaviour {

		// Use this for initialization
		public GameObject VRCam;

		private Player player = null;
		private PlayerStat playerStat;
		private GameObject head;
		//Player Status
		[HideInInspector] public bool isHitSlam;
		[HideInInspector] public Collision colInfo;
		void Start () {
			player = InteractionSystem.Player.instance;
			playerStat = player.GetComponent<PlayerStat>();
			head = VRCam.transform.FindChild("FollowHead").gameObject;
			isHitSlam = false;
		}
		
		// Update is called once per frame
		void Update () {
			HandsEvent();
		}
		void HandsEvent() {
			foreach ( Hand hand in player.hands ){
				if ( hand.controller != null )
				{
					if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
					{
						Debug.Log("pressed");
						Dash();
					}
				}
			}
		}
		void Dash() {
			Debug.Log("dash");
			if (playerStat.stamina >= 50f) {
				Vector3 direction = new Vector3(head.transform.right.x * 8.5f, 0f, head.transform.right.z * 8.5f);
				direction = Quaternion.Euler(0, -90, 0) * direction;
				player.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
				// playerStat.stamina -= 50f;
			}
		}
	}
}
