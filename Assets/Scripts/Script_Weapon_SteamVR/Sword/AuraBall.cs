using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class AuraBall : MonoBehaviour, AuraSkill {
		//ult
		public event SwordSkillFinishHandler SkillFinish;
		public float duration = 8;
		public float damage = 50.0f;
		private IEnumerator executionTime;
		private GameObject swordRef;
		private Vector3 originalScale;
		private string skillType = "ultimate";
		void Start () {
			executionTime = ExecuteTime();
		}
		public string GetSkillType(){
			return skillType;
		}
		public int GetCoolDown(){
			return 0;
		}
		public void OnColEnter(){
		}
		public void Execute(GameObject sword ){
			//store reference, initial transform blah blah
			swordRef = sword;
			//enlarge sword
			Vector3 swordScale = sword.transform.localScale;
			originalScale = new Vector3(swordScale.x,swordScale.y,swordScale.z);
			sword.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
			//find the effect in child and play it
			//start a coroutine
			StartCoroutine(executionTime);
		}
		private void Revert(){
			swordRef.transform.localScale = originalScale;
			SelfDestruct();
			//stop and revert all effects
		}
		public float GetSkillDamage(){
			return damage;
		}
		public void SelfDestruct(){
			Destroy(gameObject);
		}

		private IEnumerator ExecuteTime(){
			// 
			float dur = duration;
			while(dur > 0){
				dur -= 1;
				Debug.Log(dur);
				if(dur == 0){
					if(SkillFinish!=null){
						Debug.Log("revert");
						Revert();
						SkillFinish();
					}
				}
				yield return new WaitForSeconds(1);
			}
			
		}
	}
}
