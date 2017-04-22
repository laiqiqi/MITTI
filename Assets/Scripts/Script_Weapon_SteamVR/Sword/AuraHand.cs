using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public delegate void SwordSkillFiredHandler(int cooldown);
	public delegate void SwordUltimateFiredHandler();
	public delegate void SwordDamageDealtHandler();
	public class AuraHand : MonoBehaviour {

		// Use this for initialization
		//a list of auras
		public event SwordSkillFiredHandler SkillFired;
		public event SwordUltimateFiredHandler UltFired;
		public event SwordDamageDealtHandler DamageDealt;
		public float slotDistance = 0.2f;
		public Transform auraTransform;
        public List<GameObject> auraPrefabs;
        private Sword sword;
		private List<SkillObserver> observers = new List<SkillObserver>();
		private int auraPos = 0;
		private Hand hand;
		private bool combined;
        private GameObject currentAura;
		void Start () {
			
		}
        
		private GameObject InstantiateAuraBall(){
			//check if prefab if null, dont instantiate anything
			GameObject auraBall;
			if (auraPrefabs[auraPos] != null){
				auraBall = Instantiate( auraPrefabs[auraPos], auraTransform.position, auraTransform.rotation ) as GameObject;
				auraBall.transform.parent  = auraTransform;
			}else{
				auraBall = null;
			}
			
			return auraBall;
		}
		private void OnAttachedToHand( Hand attachedHand )
		{
			hand = attachedHand;
            FindSword();

			currentAura = InstantiateAuraBall();
			//spawn
		}
		private void HandAttachedUpdate( Hand hand )
		{
			if(!combined){
				if(sword == null){
					FindSword();
				}
				if( currentAura != null){
					float distanceToSlotPosition = Vector3.Distance( transform.parent.position, sword.auraSlotTransform.position );
					if( ( distanceToSlotPosition < slotDistance ) ){
						
						if(sword.CheckNoAura()){
						currentAura.transform.position = sword.auraSlotTransform.position;
						// if( hand.controller.GetPress( SteamVR_Controller.ButtonMask.Trigger)){
						sword.SetSkillAura(currentAura);
						
						currentAura.transform.parent = sword.auraSlotTransform;
						sword.Execute();
						AuraSkill cAuraSkill = currentAura.GetComponent<AuraSkill>();
						if(cAuraSkill.GetSkillType() == "ultimate"){
							//fire ultimate exectue
							if(UltFired != null){
								UltFired();
							}
							
						}
						else if(cAuraSkill.GetSkillType() == "normal"){
							//fire skillexecute
							if(SkillFired != null){
								SkillFired(cAuraSkill.GetCoolDown());
							}
						}
						combined = true;

						combinedAuraToSword();
						}
						// }

					}else{
						currentAura.transform.position = auraTransform.transform.position;
					}
					//check if sword null
					//check if auraball instantiate
					//check distance lerp rotate
					//check distance lerp distance
					//check if close enough to slot and press up 
						//execute sword skill + effect + parent to sword.auraslot
						//sword.execute(currentAura.GetComponent<auraskill>())
				}
				
			}
		}
		private void combinedAuraToSword(){
				currentAura = null;
				combined = false;
				auraPos = 0;

		}
		public GameObject GetCurrentAura(){
			return currentAura;
		}
        public void ChangeSkill(int auraSlot){
			if(auraSlot != -1 && auraSlot != auraPos){
				auraPos = auraSlot;
				if(currentAura!=null){
					currentAura.GetComponent<AuraSkill>().SelfDestruct();
					currentAura.transform.parent = null;
					currentAura = null;
					Debug.Log("skill changed");
				}
				currentAura = InstantiateAuraBall();
			}
		}
		private void OnDetachedFromHand( Hand hand )
		{
			//destroy canvas on Hand before destroy
			OnAboutToDestroy();
			Destroy( gameObject );
		}

		private void FindSword()
		{
			sword = hand.otherHand.GetComponentInChildren<Sword>();
			sword.OnDamageDealt += new DamageDealtHandler(onDamageCome);
			
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
