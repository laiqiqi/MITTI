using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Valve.VR.InteractionSystem
{
	public class PlayerControl : MonoBehaviour {

		// Use this for initialization
		public GameObject VRCam;
		public bool isOnFloor;

		private Player player = null;
		private PlayerStat playerStat;
		private GameObject head;
		private Antialiasing camAA;
		public bool isIncre, isDash, isDashable;
		private float timer, counter;
		public float rate, limit;
		[HideInInspector] public Collision colInfo;
		void Start () {
			timer = 3f;
			counter = 0f;
			isIncre = false;
			isDash = false;
			isOnFloor = false;
			// isDashable = false;
			player = InteractionSystem.Player.instance;
			playerStat = player.GetComponent<PlayerStat>();
			// head = VRCam.transform.FindChild("FollowHead").gameObject;
			// head = VRCam;
			camAA = VRCam.GetComponent<Antialiasing>();
		}
		
		// Update is called once per frame
		void Update () {
			HandsEvent();
			Dazzle();

			if(isDash){
				if(counter < timer){
					counter += 0.1f;
				}
				else{
					isDash = false;
					if(isOnFloor) {
						counter = 0;
						player.transform.rotation = Quaternion.EulerAngles(0, 0, 0);
						GetComponent<Rigidbody>().isKinematic = true;
					}
				}
			}
			// Debug.Log(isDashable);
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
			if (playerStat.stamina >= 50f && isDashable) {
				GetComponent<Rigidbody>().isKinematic = false;
				player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				Vector3 direction = new Vector3(VRCam.transform.right.x * 5f, 0f, VRCam.transform.right.z * 5f);
				direction = Quaternion.Euler(0, -90, 0) * direction;
				player.GetComponent<Rigidbody>().AddForce(direction, ForceMode.VelocityChange);
				playerStat.stamina -= 50f;
				isDash = true;
				isOnFloor = false;
			}
		}

		void Dazzle() {
			if(player.GetComponent<PlayerStat>().isStartDazzle){
				camAA.showGeneratedNormals = true;
				camAA.offsetScale = 2;
				camAA.blurRadius = 25;

				StartCoroutine(SetDazzleTimer());
				
				player.GetComponent<PlayerStat>().isStartDazzle = false;
				player.GetComponent<PlayerStat>().isDazzle = true;
				isIncre = true;
			}
			else if(player.GetComponent<PlayerStat>().isDazzle){
				Debug.Log("dazzle");
				if(isIncre){
					if(camAA.offsetScale > limit){
						isIncre = false;
					}
					else{
						camAA.offsetScale += rate;
						camAA.blurRadius += rate;
					}
				}
				else{
					if(camAA.offsetScale < -limit){
						isIncre = true;
					}
					else{
						camAA.offsetScale -= rate;
						camAA.blurRadius -= rate;
					}
				}
			}
		}

		IEnumerator SetDazzleTimer(){
			yield return new WaitForSeconds(12f);
			Debug.Log("StopDazzle");
			camAA.showGeneratedNormals = false;
			camAA.offsetScale = 0.2f;
			camAA.blurRadius = 18;
			player.GetComponent<PlayerStat>().isDazzle = false;
		}

		void OnTriggerEnter (Collider col) {
			if(col.tag.Equals("Floor") && !isDash){
				Debug.Log("Player landing");
				isOnFloor = true;
				isDashable = true;
			}
		}
	}
}
