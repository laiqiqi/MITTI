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
		private Vector3 originalColScale;
		private Vector3 orignalColCenter;
		private Sword sword;
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
			this.sword = sword.GetComponent<Sword>();
			BoxCollider swordCol = sword.GetComponent<BoxCollider>();
			//enlarge sword
			
			Vector3 swordScale = sword.transform.localScale;
			originalScale = new Vector3(swordScale.x,swordScale.y,swordScale.z);
			originalColScale = new Vector3(swordCol.size.x,swordCol.size.y,swordCol.size.z);
			orignalColCenter = new Vector3(swordCol.center.x,swordCol.center.y,swordCol.center.z);
			swordCol.center = new Vector3(swordCol.center.x,swordCol.center.y, 14.4f);
			swordCol.size = new Vector3(0.47f,swordCol.center.y, 27.2f);
		
// Center z 8.1-> 14.4
// Size z 14.54 -> 27.2
// size x 0.2049474 -> 0.47
			
			// this.sword.ultBlade.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
			//find the effect in child and play it
			// this.sword.blade.gameObject.SetActive(false);
			this.sword.ultBlade.gameObject.SetActive(true);
			//start a coroutine
			StartCoroutine(executionTime);
		}
		private void Revert(){
			Debug.Log("55555555555555555555555 sword reveryed bitch");
			swordRef.transform.localScale = originalScale;
			this.sword.ultBlade.gameObject.SetActive(false);
			swordRef.GetComponent<BoxCollider>().center.Set(orignalColCenter.x,orignalColCenter.y,orignalColCenter.z);
			swordRef.GetComponent<BoxCollider>().size.Set(originalColScale.x,originalColScale.y,originalColScale.z);
			// this.sword.blade.gameObject.SetActive(true);
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
