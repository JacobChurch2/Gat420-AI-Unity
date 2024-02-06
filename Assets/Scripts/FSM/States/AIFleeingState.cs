using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFleeingState : AIState
{
	public AIFleeingState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Resume();

		agent.animator?.SetTrigger("Opps");
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (agent.enemyPereption.GetGameObjects().Length > 0)
		{
			Vector3 dir = agent.transform.position - agent.enemyPereption.GetGameObjects()[0].transform.position;
			agent.movement.destination = agent.transform.position + dir;
		}
		else
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}
}
