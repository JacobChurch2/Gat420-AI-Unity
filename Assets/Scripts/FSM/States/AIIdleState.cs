using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
	public AIIdleState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		Debug.Log("Idle enter");
	}

	public override void OnExit()
	{
		Debug.Log("Idle exit");
	}

	public override void OnUpdate()
	{
		Debug.Log("Idle update");
	}
}
