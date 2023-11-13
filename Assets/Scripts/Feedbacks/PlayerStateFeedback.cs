using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFeedback : GameFeedback
{
	[SerializeField] private Unit.UnitStates _newState;
	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		gameObject.GetComponent<Unit>().CurrentState = _newState;
		yield break;
	}
}
