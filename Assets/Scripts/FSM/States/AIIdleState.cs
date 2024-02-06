using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class AIIdleState : AIState
{
	float timer;

	public AIIdleState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;

		timer = Time.time + Random.Range(1,2);
	}

	public override void OnExit()
	{
		Debug.Log("Idle exit");
	}

	public override void OnUpdate()
	{
		agent.CheckForOpps();
		if (Time.time >= timer) 
		{
			agent.stateMachine.SetState(nameof(AIPatrolState));
		}
		var enemies = agent.enemyPereption.GetGameObjects();
		if (!agent.fleer && enemies.Length > 0)
		{
			agent.stateMachine.SetState(nameof(AIChaseState));
		}

		if (agent.friendPereption)
		{
			var friends = agent.friendPereption.GetGameObjects();
			if (friends.Length > 0)
			{
				agent.stateMachine.SetState(nameof(AIWavingState));
			}
		}
	}
}
