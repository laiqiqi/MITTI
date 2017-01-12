using UnityEngine;
using System.Collections;

public interface AIState
{
	string name{ get;}
	void StartState();

	void UpdateState();

	void EndState();

	void StateChangeCondition();
}