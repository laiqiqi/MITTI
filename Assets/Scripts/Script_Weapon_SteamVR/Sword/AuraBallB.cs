using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class AuraBallB : MonoBehaviour, AuraSkill {
		//
		// Use this for initialization
		public event SwordSkillFinishHandler SkillFinish;
		public int duration = 5;
		public float damage = 20.0f;
		public int cooldown = 20;
		public float healpercent = 0.5f;
		private PlayerHealth health;
		private string skillType = "normal";
		void Start () {
			health = Player.instance.playerHealth.GetComponent<PlayerHealth>();
		}
		public int GetCoolDown(){
			return cooldown;
		}
		public string GetSkillType(){
			return skillType;
		}
		public void Execute(GameObject sword){

			//begin aura
		}
		private void Revert(){
			Debug.Log("auraball B reverted");
			SelfDestruct();
			//stop and revert all effects
		}
		public void OnColEnter(){
			health.ApplyHealing(damage * healpercent);
		}
		public float GetSkillDamage(){
			return damage;
		}
		public void SelfDestruct(){
				Destroy(gameObject);
		}

		private IEnumerator ExecuteTime(){
			// 
			int dur = duration;
			while(dur > 0){
				dur -= 1;
				if(dur == 0){
					if(SkillFinish!=null){
						Revert();
						SkillFinish();
					}
				}
			}
			yield return new WaitForSeconds(1);
		}
	}
}