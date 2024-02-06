using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIState
{
	float timer = 0;

	public AIHitState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;

		agent.animator?.SetTrigger("Hit");
		timer = Time.time + 0.5f;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{
		if (Time.time >= timer)
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	
}
