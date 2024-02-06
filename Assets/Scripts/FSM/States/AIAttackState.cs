using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
	float timer = 0;

	public AIAttackState(AIStateAgent agent) : base(agent)
	{

	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;

		agent.animator?.SetTrigger("Attack");
		timer = Time.time + 2;
		Attack();
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

	private void Attack()
	{
		// check for collision with surroundings
		var colliders = Physics.OverlapSphere(agent.transform.position, 1);
		foreach (var collider in colliders)
		{
			// don't hit self or objects with the same tag
			if (collider.gameObject == agent.gameObject || collider.gameObject.CompareTag(agent.gameObject.tag)) continue;

			// check if collider object is a state agent, reduce health
			if (collider.gameObject.TryGetComponent<AIStateAgent>(out var stateAgent))
			{
				stateAgent.ApplyDamage(Random.Range(20, 50));
			}
		}
	}
}
