using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
	Vector3 destination;

	public AIPatrolState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.movement.Resume();

		var navNode = AINavNode.GetRandomAINavNode();
		destination = navNode.transform.position;
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
		agent.CheckForOpps();
		agent.movement.MoveTowards(destination);
		if(Vector3.Distance(agent.transform.position, destination) < 1)
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}

		var enemies = agent.enemyPereption.GetGameObjects();
		if(!agent.fleer && enemies.Length > 0 )
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
   
