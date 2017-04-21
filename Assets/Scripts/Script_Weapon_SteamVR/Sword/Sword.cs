using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
   
	public class Sword : MonoBehaviour {

		// Use this for initialization
        public event DamageDealtHandler OnDamageDealt;
        public GameObject bloodBlade;
        public GameObject ultBlade;
        public GameObject blade;
        public float BaseDamage = 10.0f;
        public Transform auraSlotTransform;
		private List<SkillObserver> observers = new List<SkillObserver>();
		private Hand hand;
        private bool skillIsActive;
        private AuraSkill auraSkill;
        private GameObject aura;

	    private delegate float ApplyDamage();
        ApplyDamage applyDamage;
    	void Start () {
			applyDamage = GetBaseDamage;
		}
        void OnCollisionEnter(Collision col){
            if(col.transform.tag == "AI"){
                col.collider.gameObject.SendMessageUpwards("ApplyDamage", applyDamage(), SendMessageOptions.DontRequireReceiver);
                if(auraSkill != null && skillIsActive){
                    auraSkill.OnColEnter();
                }
            }
        }
		public void Execute(){
            //Main of apply skills of sword
            auraSkill.Execute(this.gameObject);
            //set  damage
            applyDamage = auraSkill.GetSkillDamage;
            //set skill to active
            skillIsActive = true;
            //hook event for when skill finishes
            auraSkill.SkillFinish += new SwordSkillFinishHandler(OnSkillFinish);
            
		}

        void OnSkillFinish(){
            applyDamage = GetBaseDamage;
            skillIsActive = false;
            aura = null;
            auraSkill = null;
        }

        public bool CheckNoAura(){
            if(aura == null){
                return true;
            }else{
                return false;
            }
        }

        public void SetSkillAura(GameObject aura){
            this.aura = aura;
            auraSkill = aura.GetComponent<AuraSkill>();
        }

        float GetBaseDamage(){
            //apply base damage to AI
            //ONLY LET SKILL UI KNOW WHEN DAMAGE DEALT TRIGGERS BY BASE DAMAGE ONLY (NOT SKILL NOT ULTIMATE)
            if(OnDamageDealt != null){
                OnDamageDealt(BaseDamage);
            }
            return BaseDamage;
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
			OnAboutToDestroy();
			Destroy( gameObject );
		}
			//-------------------------------------------------
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
	}
}
