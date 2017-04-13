using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class Sword : MonoBehaviour {

		// Use this for initialization
		private List<SkillObserver> observers = new List<SkillObserver>();
		private Hand hand;
		void Start () {
			
		}

		public void ChangeSkill(int arrowSlot){

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
