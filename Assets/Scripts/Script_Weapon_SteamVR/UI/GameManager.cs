using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	public class GameManager : MonoBehaviour {

		// Use this for initialization
		public float slideTrans;
		private Vector2 releasedDir;
		private Vector2 prevFramePosition;

		private float prevFrameVelocity;

		private float frameAcceleration;
		public float CSlideSpeed;

		private bool sliding = false;

		private float lstart = 3;

		private float lend = -3;
		private Transform box;

		private float pos = -1;
		private Coroutine MoveToCo;
		private float startBox;
		
		private float nextTime = 0;
		private float interval = 0.2f;
		private Vector2 slideStartPos;
		private Coroutine inventoryHint = null;
		private Player player = null;
		void Start () {
			box = gameObject.transform.GetChild(0).transform;

			player = InteractionSystem.Player.instance;

			if ( player == null )
			{
				Debug.LogError( "Teleport: No Player instance found in map." );
				Destroy( this.gameObject );
				return;
			}

			// Invoke("ShowInventoryHint", 1.0f);
		}
		
		private void ShowInventoryHint(){
			inventoryHint = StartCoroutine(HintCoroutine());
		}

		private bool IsEligibleForTeleport(Hand hand){
			return true;
		}

		private IEnumerator HintCoroutine(){
			float prevBreakTime = Time.time;
			float prevHapticPulseTime = Time.time;

			while ( true )
			{
				bool pulsed = false;

				//Show the hint on each eligible hand
				foreach ( Hand hand in player.hands )
				{
					bool showHint = IsEligibleForTeleport( hand );
					bool isShowingHint = !string.IsNullOrEmpty( ControllerButtonHints.GetActiveHintText( hand, EVRButtonId.k_EButton_SteamVR_Touchpad ) );
					if ( showHint )
					{
						if ( !isShowingHint )
						{
							ControllerButtonHints.ShowTextHint( hand, EVRButtonId.k_EButton_SteamVR_Touchpad, "place it behind" );
							prevBreakTime = Time.time;
							prevHapticPulseTime = Time.time;
						}

						if ( Time.time > prevHapticPulseTime + 0.05f )
						{
							//Haptic pulse for a few seconds
							pulsed = true;

							hand.controller.TriggerHapticPulse( 500 );
						}
					}
					else if ( !showHint && isShowingHint )
					{
						ControllerButtonHints.HideTextHint( hand, EVRButtonId.k_EButton_SteamVR_Touchpad );
					}
				}

				if ( Time.time > prevBreakTime + 3.0f )
				{
					//Take a break for a few seconds
					yield return new WaitForSeconds( 3.0f );

					prevBreakTime = Time.time;
				}

				if ( pulsed )
				{
					prevHapticPulseTime = Time.time;
				}

				yield return null;
			}
		}
		// Update is called once per frame
		void Update () {
			foreach ( Hand hand in player.hands ){
				if(hand.controller != null){
					
					if (hand.controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad)){
						slideStartPos = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
						startBox = box.transform.localPosition.x;

						//
						// prevFramePosition = slideStartPos;
						// prevFrameVelocity = 0f;
					}
					else if(hand.controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad )){
						Vector3 slidedir = (hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad) - slideStartPos).normalized;
						Vector3 slideDist = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad) - slideStartPos;
						
						if (Mathf.Abs(slideDist.x) > .1f || sliding){
							if(!sliding){
								sliding = true;
							}
							if((hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x != prevFramePosition.x)){
								Debug.Log("current x: " + hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x + "current y: " + hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y);
								releasedDir = (hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad) - prevFramePosition).normalized;
								float frameVelocity = (hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x - prevFramePosition.x) / Time.deltaTime;
								// frameAcceleration = Mathf.Abs(frameVelocity - prevFrameVelocity)/Time.deltaTime;
								// Debug.Log("current pos: " + hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x + "|prev pos:" + prevFramePosition + "|dist:" + (hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x - prevFramePosition));
								// Debug.Log(hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x - prevFramePosition);
								// nextTime += interval;
								
								// prevFramePosition = hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
								prevFrameVelocity = frameVelocity;
							}
								// if(slidedir.x < 0 && pos == -1 || slidedir.x > 0 && pos == 1){
								// 	box.transform.localPosition = new Vector3(startBox + slideDist.x / 2, box.transform.localPosition.y ,box.transform.localPosition.z);
								// }else{
								// 	box.transform.localPosition = new Vector3(startBox + slideDist.x * 1f, box.transform.localPosition.y ,box.transform.localPosition.z);
								// }
								box.transform.localPosition = new Vector3(startBox + slideDist.x * slideTrans, box.transform.localPosition.y ,box.transform.localPosition.z);
						}
					}else if(hand.controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad )){
						if(sliding){
							Debug.Log("releasedDir" + releasedDir);
							// Debug.Log("frameVelocity: " + prevFrameVelocity);
							// Debug.Log("frameAcceleration: " + frameAcceleration);

							// float dend = lend - box.transform.localPosition.x;
							// float dstart = lstart - box.transform.localPosition.x;
							float dcenter = 0f - box.transform.localPosition.x;
							// Debug.Log((hand.controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x - slideStartPos.x)/(Time.time - slideStartTime));
							
							MoveToCo = StartCoroutine(MoveTo(dcenter, dcenter * .01f * CSlideSpeed));
							// if(Mathf.Abs(dend) <= Mathf.Abs(dstart)){
							// 	if(frameAcceleration > 300f && releasedDir.x > 0){
							// 		pos = 1;
							// 		MoveToCo = StartCoroutine(MoveTo(dstart, dstart * .01f * CSlideSpeed));
							// 	}else{
							// 		pos = -1;
							// 		MoveToCo = StartCoroutine(MoveTo(dend, dend * .01f * CSlideSpeed));
							// 	}
							// }
							// else{
							// 	if(frameAcceleration > 300f && releasedDir.x < 0){
							// 		pos = -1;
							// 		MoveToCo = StartCoroutine(MoveTo(dend, dend * .01f * CSlideSpeed));
							// 	}else{
							// 		pos = 1;
							// 		MoveToCo = StartCoroutine(MoveTo(dstart, dstart * .01f * CSlideSpeed));
							// 	}
							// }
						}
						sliding = false;
					}
				}
			}
		}
		
		IEnumerator MoveTo(float DistTargetPos, float i){
			var x = 0f;
			while(Mathf.Abs(DistTargetPos) >= Mathf.Abs(x)){
				var traverse = i;
				if (Mathf.Abs(x+i) > Mathf.Abs(DistTargetPos)){
					traverse = (Mathf.Abs(DistTargetPos) - Mathf.Abs(x)) * (i/Mathf.Abs(i)); 
				}
				x += traverse;
				box.transform.Translate(traverse, 0, 0);
				yield return null;
			}
			
		}
	}
}