﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
   
	public class Sword : MonoBehaviour {

		// Use this for initialization
        public event DamageDealtHandler OnDamageDealt;
        public ParticleSystem hitAIParticle;
        public ParticleSystem hitMagParticle;
        public GameObject bloodBlade;
        public GameObject ultBlade;
        public GameObject blade;
        public float BaseDamage = 10.0f;
        public float CritDamage = 20.0f;
        public Transform auraSlotTransform;
		private List<SkillObserver> observers = new List<SkillObserver>();
		private Hand hand;
        private bool skillIsActive;
        private AuraSkill auraSkill;
        private GameObject aura;
        private bool ableToHitAI = true;

	    private delegate float ApplyDamage();
        ApplyDamage applyDamage;
    	void Start () {
			applyDamage = GetBaseDamage;
		}
        void OnCollisionEnter(Collision col){
            bool hitMagnetFirst = false;
            if(col.transform.tag == "Magnet" && ableToHitAI){
                hitMagnetFirst = true;
            }
            if(col.transform.tag == "AI" && ableToHitAI){
                //hit particle
                if(hitMagnetFirst){
                    ParticleSystem part1 = Instantiate(hitMagParticle);
                    part1.gameObject.GetComponent<ParticleSelfDestruct>().enabled = true;
                    part1.gameObject.transform.parent = null;
                    part1.gameObject.transform.position = col.contacts[0].point;
                    part1.Play();
                }else{
                    ParticleSystem part = Instantiate(hitAIParticle);
                    part.gameObject.GetComponent<ParticleSelfDestruct>().enabled = true;
                    part.gameObject.transform.parent = null;
                    part.gameObject.transform.position = col.contacts[0].point;
                    part.Play();
                }
                //apply dmg
                if(skillIsActive){
                    col.collider.gameObject.SendMessageUpwards("ApplyDamage", applyDamage(), SendMessageOptions.DontRequireReceiver);
                }else{
                    if(hitMagnetFirst){
                        col.collider.gameObject.SendMessageUpwards("ApplyDamage", applyDamage(), SendMessageOptions.DontRequireReceiver);
                    }else{
                        col.collider.gameObject.SendMessageUpwards("ApplyDamage", CritDamage, SendMessageOptions.DontRequireReceiver);
                    }
                }

                //apply on hit effects
                if(auraSkill != null && skillIsActive){
                    auraSkill.OnColEnter();
                }
                ableToHitAI = false;
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
        //call by AISwordRadiusCheck.cs
        void SetAbleHitAI(bool f){
            ableToHitAI = f;
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
