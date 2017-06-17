using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public delegate void SwordSkillFinishHandler();


	public interface AuraSkill {
		event SwordSkillFinishHandler SkillFinish;
		void Execute(GameObject sword);
		void OnColEnter();
		string GetName();
		float GetSkillDamage();
		void SelfDestruct();
		string GetSkillType();
		int GetCoolDown();
	}
}