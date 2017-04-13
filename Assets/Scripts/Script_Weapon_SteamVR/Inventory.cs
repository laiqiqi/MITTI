using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{

	public delegate void WeaponChangeHandler(GameObject spawnedObject, Canvas spawnedUI);
	
	[RequireComponent( typeof( Interactable ) )]

	public class Inventory : MonoBehaviour {
		public event WeaponChangeHandler WeaponChanged;
		public List<ItemPackage> AllItemPackage;
		
		[EnumFlags]
		public Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags;
		public string attachmentPoint;

		public bool requireTriggerPressToTake;
		private GameObject spawnedItem;
		private GameObject UIspawnedItem;
		private Canvas spawnedUI;
		private ItemPackage itemPackage;
		int weaponArrayPos = 0;
		Hand currentHand = null;
		// Use this for initialization
		void Start () {
			itemPackage = AllItemPackage[weaponArrayPos];
		}
		
		// Update is called once per frame
		void Update () {
			// foreach ( Hand hand in player.hands ){
			// 	if(hand.controller != null){
			// 		OnTouchPadEnter(hand);
			// 		OnTouchPadStay(hand);
			// 		OnTouchUp(hand);
			// 	}
			// }
		}

		private void OnWeaponChanged(GameObject newWeapon, Canvas newUI){
			if(WeaponChanged != null){
				WeaponChanged(newWeapon, newUI);
			}
		}

		void updateWeaponPos(){
			if (weaponArrayPos + 1 >= AllItemPackage.Count){
				weaponArrayPos = 0;
				itemPackage = AllItemPackage[weaponArrayPos];
				return;
			}
			weaponArrayPos++;
			itemPackage = AllItemPackage[weaponArrayPos];
		}

		private void HandHoverUpdate( Hand hand )
		{
			if ( requireTriggerPressToTake )
			{
				if ( hand.controller != null && hand.controller.GetHairTriggerDown() )
				{
					SpawnAndAttachObject( hand );
				}
				if ( hand.controller != null && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip )  )
				{
					RemoveAllObject(hand);
					
				}
			}
		}

		private ItemPackage GetAttachedItemPackage( Hand hand )
		{
			GameObject currentAttachedObject = hand.currentAttachedObject;

			if ( currentAttachedObject == null ) // verify the hand is holding something
			{
				return null;
			}

			ItemPackageReference packageReference = hand.currentAttachedObject.GetComponent<ItemPackageReference>();
			if ( packageReference == null ) // verify the item in the hand is matchable
			{
				return null;
			}

			ItemPackage attachedItemPackage = packageReference.itemPackage; // return the ItemPackage reference we find.

			return attachedItemPackage;
		}

		private void TakeBackItem( Hand hand )
		{
			RemoveMatchingItemsFromHandStack( itemPackage, hand );

			if ( itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded )
			{
				RemoveMatchingItemsFromHandStack( itemPackage, hand.otherHand );
			}
		}			
		private void SpawnAndAttachItemSkillUI(Hand hand){
			//attach canvas to hand
			if(itemPackage.itemUI){
				spawnedUI = Canvas.Instantiate(itemPackage.itemUI);
				spawnedUI.GetComponent<SkillObserver>().AddObjectInstance(UIspawnedItem);
				spawnedUI.gameObject.SetActive(true);
				hand.AttachUI(spawnedUI);
			}
			
			//clean up UIspawnedItem - somehow remove it from here
			//clean up spawnedUI
			 
		}

		private void RemoveAllObject(Hand hand){
			RemoveAllItemsFromHand( hand );
			RemoveAllItemsFromHand( hand.otherHand );
			OnWeaponChanged(null, null);
		}
		private void SpawnAndAttachObject( Hand hand )
		{
			if ( hand.otherHand != null )
			{
				//If the other hand has this item package, take it back from the other hand
				ItemPackage otherHandItemPackage = GetAttachedItemPackage( hand.otherHand );
				if ( otherHandItemPackage == itemPackage )
				{
					TakeBackItem( hand.otherHand );
				}
			}

			// if ( showTriggerHint )
			// {
			// 	ControllerButtonHints.HideTextHint( hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger );
			// }

			// if ( itemPackage.otherHandItemPrefab != null )
			// {
			// 	if ( hand.otherHand.hoverLocked )
			// 	{
					//Debug.Log( "Not attaching objects because other hand is hoverlocked and we can't deliver both items." );
			// 		return;
			// 	}
			// }

			// if we're trying to spawn a one-handed item, remove one and two-handed items from this hand and two-handed items from both hands
			if ( itemPackage.packageType == ItemPackage.ItemPackageType.OneHanded )
			{
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.OneHanded, hand );
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.TwoHanded, hand );
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.TwoHanded, hand.otherHand );
			}

			// if we're trying to spawn a two-handed item, remove one and two-handed items from both hands
			if ( itemPackage.packageType == ItemPackage.ItemPackageType.TwoHanded )
			{
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.OneHanded, hand );
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.OneHanded, hand.otherHand );
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.TwoHanded, hand );
				RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType.TwoHanded, hand.otherHand );
			}

			spawnedItem = GameObject.Instantiate( itemPackage.itemPrefab );
			spawnedItem.SetActive( true );
			hand.AttachObject( spawnedItem, attachmentFlags, attachmentPoint );
			//create Canvas UI 
			// spawnedUI = Canvas.Instantiate(itemPackage.skillSets);
			// spawnedUI.gameObject.SetActive(true);
			// hand.AttachUI(spawnedUI);
			//update spawnedObject into canvas
			//UI spawneditem is the object the UI has to talk to
			//right now its hard coded as the first object or the second object
			//if its sword, just talk to the sword, if its bow, talk to the arrow hand
			//not the long bow
			UIspawnedItem = spawnedItem;
			

			

			if ( ( itemPackage.otherHandItemPrefab != null ) && ( hand.otherHand.controller != null ) )
			{
				GameObject otherHandObjectToAttach = GameObject.Instantiate( itemPackage.otherHandItemPrefab );
				otherHandObjectToAttach.SetActive( true );
				hand.otherHand.AttachObject( otherHandObjectToAttach, attachmentFlags );
				UIspawnedItem = otherHandObjectToAttach;
				//add spawnedObject into canvas
			}
			//add the event
			
			SpawnAndAttachItemSkillUI( hand );
			OnWeaponChanged(UIspawnedItem, spawnedUI);
			UIspawnedItem = null;
			spawnedUI = null;
			updateWeaponPos();
		}

			private void RemoveMatchingItemsFromHandStack( ItemPackage package, Hand hand )
		{
			for ( int i = 0; i < hand.AttachedObjects.Count; i++ )
			{
				ItemPackageReference packageReference = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
				if ( packageReference != null )
				{
					ItemPackage attachedObjectItemPackage = packageReference.itemPackage;
					if ( ( attachedObjectItemPackage != null ) && ( attachedObjectItemPackage == package ) )
					{
						GameObject detachedItem = hand.AttachedObjects[i].attachedObject;
						hand.DetachObject( detachedItem );
					}
				}
			}
		}


		//-------------------------------------------------
		private void RemoveMatchingItemTypesFromHand( ItemPackage.ItemPackageType packageType, Hand hand )
		{
			for ( int i = 0; i < hand.AttachedObjects.Count; i++ )
			{
				ItemPackageReference packageReference = hand.AttachedObjects[i].attachedObject.GetComponent<ItemPackageReference>();
				if ( packageReference != null )
				{
					if ( packageReference.itemPackage.packageType == packageType )
					{
						GameObject detachedItem = hand.AttachedObjects[i].attachedObject;
						hand.DetachObject( detachedItem );
					}
				}
			}
		}

		private void RemoveAllItemsFromHand(Hand hand){
			for ( int i = 1; i < hand.AttachedObjects.Count; i++ )
			{
				GameObject detachedItem = hand.AttachedObjects[i].attachedObject;
				hand.DetachObject( detachedItem );
				Debug.Log(detachedItem);
			}
		}
		
	}
}