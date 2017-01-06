using UnityEngine;
using System.Collections;

public interface AIState
{
	void StartState();

	void UpdateState();

	void EndState();

	void StateChangeCondition();
}