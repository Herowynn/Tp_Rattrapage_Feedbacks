using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnotherGameEvent : GameFeedback
{
	[SerializeField, SerializeReference] private GameEvent _feedback;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		GameEventsManager.Instance.PlayEvent(_feedback.name, gameObject);
		yield break;
	}
}
