using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AIStateMachine
{
	private Dictionary<string, AIState> states = new Dictionary<string, AIState>();

	public AIState CurrentState { get; private set; } = null;

	public void Update()
	{
		CurrentState?.OnUpdate();
	}

	public void AddState(string name, AIState state)
	{
		Debug.Assert(!states.ContainsKey(name), "State machine already contains state " + name);

		states[name] = state;
	}

	public void SetState(string name)
	{
		Debug.Assert(states.ContainsKey(name), "State machine does not contain state " + name);

		var state = states[name];

		//dont reenter same state
		if (state == CurrentState) return;

		//exit current state
		CurrentState?.OnExit();
		//set new state
		CurrentState = state;
		//enter new state
		CurrentState?.OnEnter();
	}
}
