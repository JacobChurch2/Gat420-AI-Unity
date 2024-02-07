using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDancingState : AIState
{
	float timer;

	public AIDancingState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;

		agent.animator?.SetTrigger("Dance");
		timer = Time.time + 3;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{
		//agent.CheckForOpps();
		if(Time.time > timer)
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}
}
