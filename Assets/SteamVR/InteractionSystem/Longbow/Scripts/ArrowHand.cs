//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The object attached to the player's hand that spawns and fires the
//			arrow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public delegate void UltimateFireHandler();
	public delegate void DamageHandler();
	public delegate void SkillFireHandler(int cooldown);
	public delegate void ArrowAttachedHandler();
	public delegate void ArrowFiredHandler();
	public delegate void SkillChangeHandler();
	public class ArrowHand : MonoBehaviour
	{
		private Hand hand;
		private Longbow bow;
		private Skill skill;
		public event UltimateFireHandler UltimateFired;
		public event DamageHandler DamageDealt;
		public event SkillFireHandler SkillFired;
		public event ArrowAttachedHandler ArrowAttached;
		public event ArrowFiredHandler ArrowFired;
		public event SkillChangeHandler SkillChange;
		private GameObject currentArrow;
		public GameObject arrowPrefab;
		public List<GameObject> arrowPrefabs;
		public Transform arrowNockTransform;

		public float nockDistance = 0.1f;
		public float lerpCompleteDistance = 0.08f;
		public float rotationLerpThreshold = 0.15f;
		public float positionLerpThreshold = 0.15f;

		private bool allowArrowSpawn = true;
		private bool nocked;

		private int selectedArrow = 0;
		private bool inNockRange = false;
		private bool arrowLerpComplete = false;

		private List<SkillObserver> observers = new List<SkillObserver>();
		public SoundPlayOneshot arrowSpawnSound;

		private AllowTeleportWhileAttachedToHand allowTeleport = null;

		public int maxArrowCount = 10;
		private List<GameObject> arrowList;

	


		//-------------------------------------------------
		void Awake()
		{
			allowTeleport = GetComponent<AllowTeleportWhileAttachedToHand>();
			allowTeleport.teleportAllowed = true;
			allowTeleport.overrideHoverLock = false;

			arrowList = new List<GameObject>();
		}


		//-------------------------------------------------
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
			FindBow();
		}


		//-------------------------------------------------
		private GameObject InstantiateArrow()
		{
			GameObject arrow = Instantiate( arrowPrefabs[selectedArrow], arrowNockTransform.position, arrowNockTransform.rotation ) as GameObject;
			//add damage listener
				if(arrow.GetComponent<WeaponDamage>()!=null)
				arrow.GetComponent<WeaponDamage>().OnDamageDealt += new DamageDealtHandler(onDamageCome);
			//
			arrow.name = "Bow Arrow";
			arrow.transform.parent = arrowNockTransform;
			Util.ResetTransform( arrow.transform );

			arrowList.Add( arrow );

			while ( arrowList.Count > maxArrowCount )
			{
				GameObject oldArrow = arrowList[0];
				arrowList.RemoveAt( 0 );
				if ( oldArrow )
				{
					Destroy( oldArrow );
				}
			}

			return arrow;
		}


		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			if ( bow == null )
			{
				FindBow();
			}

			if ( bow == null )
			{
				return;
			}

			if ( allowArrowSpawn && ( currentArrow == null ) ) // If we're allowed to have an active arrow in hand but don't yet, spawn one
			{
				currentArrow = InstantiateArrow();
				if(currentArrow.GetComponent<Skill>() != null){
					skill = currentArrow.GetComponent<Skill>();
				}
				else{
					skill = null;
				}
				arrowSpawnSound.Play();
			}

			float distanceToNockPosition = Vector3.Distance( transform.parent.position, bow.nockTransform.position );

			// If there's an arrow spawned in the hand and it's not nocked yet
			if ( !nocked )
			{
				// If we're close enough to nock position that we want to start arrow rotation lerp, do so
				if ( distanceToNockPosition < rotationLerpThreshold )
				{
					float lerp = Util.RemapNumber( distanceToNockPosition, rotationLerpThreshold, lerpCompleteDistance, 0, 1 );

					arrowNockTransform.rotation = Quaternion.Lerp( arrowNockTransform.parent.rotation, bow.nockRestTransform.rotation, lerp );
				}
				else // Not close enough for rotation lerp, reset rotation
				{
					arrowNockTransform.localRotation = Quaternion.identity;
				}

				// If we're close enough to the nock position that we want to start arrow position lerp, do so
				if ( distanceToNockPosition < positionLerpThreshold )
				{
					float posLerp = Util.RemapNumber( distanceToNockPosition, positionLerpThreshold, lerpCompleteDistance, 0, 1 );

					posLerp = Mathf.Clamp( posLerp, 0f, 1f );

					arrowNockTransform.position = Vector3.Lerp( arrowNockTransform.parent.position, bow.nockRestTransform.position, posLerp );
				}
				else // Not close enough for position lerp, reset position
				{
					arrowNockTransform.position = arrowNockTransform.parent.position;
				}


				// Give a haptic tick when lerp is visually complete
				if ( distanceToNockPosition < lerpCompleteDistance )
				{
					if ( !arrowLerpComplete )
					{
						arrowLerpComplete = true;
						hand.controller.TriggerHapticPulse( 500 );
					}
				}
				else
				{
					if ( arrowLerpComplete )
					{
						arrowLerpComplete = false;
					}
				}

				// Allow nocking the arrow when controller is close enough
				if ( distanceToNockPosition < nockDistance )
				{
					if ( !inNockRange )
					{
						inNockRange = true;
						bow.ArrowInPosition();
					}
				}
				else
				{
					if ( inNockRange )
					{
						inNockRange = false;
					}
				}

				// If arrow is close enough to the nock position and we're pressing the trigger, and we're not nocked yet, Nock
				if ( ( distanceToNockPosition < nockDistance ) && hand.controller.GetPress( SteamVR_Controller.ButtonMask.Trigger ) && !nocked )
				{
					if ( currentArrow == null )
					{
						currentArrow = InstantiateArrow();
						if(currentArrow.GetComponent<Skill>() != null){
							skill = currentArrow.GetComponent<Skill>();
						}
						else{
							skill = null;
						}
					}
					if(ArrowAttached!=null){
						ArrowAttached();
					}
					nocked = true;
					bow.StartNock( this );
					hand.HoverLock( GetComponent<Interactable>() );
					allowTeleport.teleportAllowed = false;
					currentArrow.transform.parent = bow.nockTransform;
					Util.ResetTransform( currentArrow.transform );
					Util.ResetTransform( arrowNockTransform );
				}
			}

			if (nocked && hand.controller.GetPress( SteamVR_Controller.ButtonMask.Trigger )){
				NowDisableUI();
				// Skill skill = currentArrow.GetComponent<Skill>();
				
				if(skill != null){
					if(bow.chargepulled){//if bow pull far back enough
						if (!skill.OverCharged() && !skill.GetChargingStatus()){
							skill.Charging();
						}
					}else{
						if(skill.GetFullyCharged()){ //if skill has been charged, give it some distance before it uncharges
							if(!bow.uncharged){ //distance until it reaches set uncharged distance 
								if(skill.GetChargingStatus()){
									skill.Uncharging(false);
								}	
							}
						}
						else if(skill.GetChargingStatus()){//if it is not charging, then decrease right away
							skill.Uncharging(false);
						}else if(skill.OverCharged()){//once the bow overcharge (like in monster hunter arrow charge last 3s)
							if(!bow.uncharged){//reset it at a fixed uncharged distance
								skill.ResetOverCharge();
							}
						}	
					}
				}
			}


			// If arrow is nocked, and we release the trigger
			if ( nocked && ( !hand.controller.GetPress( SteamVR_Controller.ButtonMask.Trigger ) || hand.controller.GetPressUp( SteamVR_Controller.ButtonMask.Trigger ) ) )
			{
				NowEnableUI();
				if ( bow.pulled ) // If bow is pulled back far enough, fire arrow, otherwise reset arrow in arrowhand
				{
					
					if (skill != null){
						if(skill.GetFullyCharged()){
							skill.Uncharging(true);
							FireArrow();
							if(skill.GetSkillType() == "ultimate"){
								if(UltimateFired != null){
									UltimateFired();
								}
							}else if(skill.GetSkillType() == "normal"){
								if(SkillFired != null){
									SkillFired(skill.GetCoolDown());
								}
							}
							NowSkillStart();
							skill = null;
						}else{
							//skill notcharged so it doesn't release
							skill.Uncharging(false);
							arrowNockTransform.rotation = currentArrow.transform.rotation;
							currentArrow.transform.parent = arrowNockTransform;
							Util.ResetTransform( currentArrow.transform );
							nocked = false;
							bow.ReleaseNock();
							hand.HoverUnlock( GetComponent<Interactable>() );
							allowTeleport.teleportAllowed = true;
						}
						
					}else{
						FireArrow();
					}
					//initiate cooldown if the skill of current arrow is fully charged
					// check if skill is not null
					
				}
				else
				{
					arrowNockTransform.rotation = currentArrow.transform.rotation;
					currentArrow.transform.parent = arrowNockTransform;
					Util.ResetTransform( currentArrow.transform );
					nocked = false;
					bow.ReleaseNock();
					hand.HoverUnlock( GetComponent<Interactable>() );
					allowTeleport.teleportAllowed = true;
				}

				bow.StartRotationLerp(); // Arrow is releasing from the bow, tell the bow to lerp back to controller rotation
			}
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			//destroy canvas on Hand before destroy
			OnAboutToDestroy();
			Destroy( gameObject );
		}


		//-------------------------------------------------
		private void FireArrow()
		{
			currentArrow.transform.parent = null;

			Arrow arrow = currentArrow.GetComponent<Arrow>();
			arrow.shaftRB.isKinematic = false;
			arrow.shaftRB.useGravity = true;
			arrow.shaftRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.isKinematic = false;
			arrow.arrowHeadRB.useGravity = true;
			arrow.arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;

			arrow.arrowHeadRB.AddForce( currentArrow.transform.forward * bow.GetArrowVelocity(), ForceMode.VelocityChange );
			arrow.arrowHeadRB.AddTorque( currentArrow.transform.forward * 10 );

			nocked = false;

			currentArrow.GetComponent<Arrow>().ArrowReleased( bow.GetArrowVelocity() );
			bow.ArrowReleased();
			if(ArrowFired != null){
				ArrowFired();
			}

			allowArrowSpawn = false;
			Invoke( "EnableArrowSpawn", 0.5f );
			StartCoroutine( ArrowReleaseHaptics() );

			currentArrow = null;
			allowTeleport.teleportAllowed = true;
		}


		//-------------------------------------------------
		private void EnableArrowSpawn()
		{
			allowArrowSpawn = true;
		}


		//-------------------------------------------------
		private IEnumerator ArrowReleaseHaptics()
		{
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 1500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 800 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 500 );
			yield return new WaitForSeconds( 0.05f );

			hand.otherHand.controller.TriggerHapticPulse( 300 );
		}


		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}


		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
			gameObject.SetActive( true );
		}


		//-------------------------------------------------
		private void FindBow()
		{
			bow = hand.otherHand.GetComponentInChildren<Longbow>();
			
		}

		public void ChangeSkill(int arrowSlot){
			if(arrowSlot != -1 && arrowSlot != selectedArrow){
				selectedArrow = arrowSlot;
				if(currentArrow!=null){
					currentArrow.GetComponent<Arrow>().SelfDestruct();
					currentArrow.transform.parent = null;
					currentArrow = null;
					if(SkillChange != null){
						SkillChange();
					}
					// Debug.Log("skill changed");
				}
				
			}
			
			
			
			// if negative one is return, do not do anything
			// and if its the same as the current don't change anything
			//change arrow
			//Destroy current Arrow - currentarrow.destroy
			//check what this gives - maybe have to null first
			//switch to selected arrow -
			//call method InstantiateArrow
		}

		public void UpdateSelectedSkill(int arrowSlot){
			if(arrowSlot != -1 && arrowSlot != selectedArrow){
				selectedArrow = arrowSlot;
			}
		}
		private void NowSkillStart()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnSkillStart();
            }
        }
		private void NowEnableUI()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnNowEnable();
            }
        }

		private void NowDisableUI()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnNowDisable();
            }
        }

		private void OnAboutToDestroy(){
			for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnObjectDetached();
            }
		}
        //Add observer to the list
        public void AddObserver(SkillObserver observer)
        {
            observers.Add(observer);
        }

        //Remove observer from the list
        public void RemoveObserver(SkillObserver observer)
        {
			observers.Remove(observer);
        }

		private void onDamageCome(float dmg){
			if(DamageDealt!=null){
				DamageDealt();
			}
		}

	}
}
