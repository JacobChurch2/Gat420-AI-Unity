using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : AIState
{
	float initialSpeed;

	public AIChaseState(AIStateAgent agent) : base(agent)
	{

	}

	public override void OnEnter()
	{
		initialSpeed = agent.movement.maxSpeed;
		agent.movement.maxSpeed *= 2;
	}

	public override void OnExit()
	{
		agent.movement.maxSpeed = initialSpeed;
	}

	public override void OnUpdate()
	{
		var enemies = agent.enemyPereption.GetGameObjects();
		if(enemies.Length > 0)
		{
			var enemy = enemies[0];
			if(Vector3.Distance(agent.transform.position, enemy.transform.position) < 1.25f)
			{
				agent.stateMachine.SetState(nameof(AIAttackState));
			}
		}
		else
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}
}
