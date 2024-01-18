using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class AIAutonomousAgent : AIAgent
{
    [SerializeField] AIPerception seekPerception = null;
	[SerializeField] AIPerception fleePerception = null;
	[SerializeField] AIPerception flockPerception = null;
	[SerializeField] AIPerception obstaclePerception = null;

    private void Update()
    {
		//seek
		if (seekPerception != null)
		{
			var gameObjects = seekPerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Seek(gameObjects[0]));
			}
		}

		//flee
		if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Flee(gameObjects[0]));
            }
        }

		//flock
		if(flockPerception != null)
		{
			var gameObjects = flockPerception.GetGameObjects();
			if(gameObjects.Length > 0)
			{
				movement.ApplyForce(Cohesion(gameObjects));
				movement.ApplyForce(Separation(gameObjects, 3));
				movement.ApplyForce(Alignment(gameObjects));
			}	
		}

		//obstacle avoidance
		if (obstaclePerception != null)
		{
			if(((AISpherecastPerception)obstaclePerception).CheckDirection(Vector3.forward))
			{
				Vector3 open = Vector3.zero;
				if (((AISpherecastPerception)obstaclePerception).GetOpenDirection(ref open))
				{
					movement.ApplyForce(GetSteeringForce(open) * 100);
				}
			}
			var gameObjects = obstaclePerception.GetGameObjects();

		}

		//cancel y movement
		Vector3 acceleration = movement.acceleration;
		acceleration.y = 0;
		movement.acceleration = acceleration;

        //transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        return GetSteeringForce(direction);
    }

	private Vector3 Flee(GameObject target)
	{
		Vector3 direction = transform.position - target.transform.position;
		return GetSteeringForce(direction);
	}

	private Vector3 Cohesion(GameObject[] neighbors)
	{
		Vector3 positions = Vector3.zero;
		foreach(var neighbor in neighbors)
		{
			positions += neighbor.transform.position;
		}

		Vector3 center = positions / neighbors.Length;
		Vector3 direction = center - transform.position;
		
		return GetSteeringForce(direction);
	}

	private Vector3 Separation(GameObject[] neighbors, float radius)
	{
		Vector3 separation = Vector3.zero;
		foreach(var neighbor in neighbors)
		{
			Vector3 direction = (transform.position - neighbor.transform.position);
			if (direction.magnitude < radius)
			{
				separation += direction / direction.sqrMagnitude;
			}
		}

		return GetSteeringForce(separation);
	}

	private Vector3 Alignment(GameObject[] neighbors)
	{
		Vector3 velocities = Vector3.zero;
		foreach(var neighbor in neighbors)
		{
			velocities += neighbor.GetComponent<AIAgent>().movement.velocity;
		}

		Vector3 averageVelocity = velocities / neighbors.Length;

		return GetSteeringForce(averageVelocity);
	}

	private Vector3 GetSteeringForce(Vector3 direction)
	{
		Vector3 desired = direction.normalized * movement.maxSpeed;
		Vector3 steer = desired - movement.velocity;
		Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

		return force;

	}
}

