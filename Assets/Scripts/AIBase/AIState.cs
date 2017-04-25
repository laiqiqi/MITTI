using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface AIState
{
	string name{ get;}
	List<AIState> choice{ get;set; }
	float stateDelay{ get; set;}
	void StartState();

	void UpdateState();

	void EndState();

	void StateChangeCondition();
}