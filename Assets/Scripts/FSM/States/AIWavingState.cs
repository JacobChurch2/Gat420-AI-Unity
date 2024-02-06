using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIWavingState : AIState
{
	float timer = 0;
	public AIWavingState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;

		agent.animator?.SetTrigger("Wave");
		timer = Time.time + 1;
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{
		agent.CheckForOpps();
		if (Time.time > timer) 
		{
			agent.stateMachine.SetState(nameof(AIDancingState));
		}
	}
}
