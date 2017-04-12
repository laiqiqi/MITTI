using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class Sword : MonoBehaviour {

		// Use this for initialization
		private Hand hand;
		void Start () {
			
		}
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
		}
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
			OnAttachedToHand( hand );
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}

		
			//-------------------------------------------------
		
	}
}
