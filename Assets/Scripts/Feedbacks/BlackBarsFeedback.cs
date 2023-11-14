using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBarsFeedback : GameFeedback
{
	[SerializeField] private bool _isVisible;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		GameManager.BlackBars.SetActive(_isVisible);
		yield break;
	}
}
