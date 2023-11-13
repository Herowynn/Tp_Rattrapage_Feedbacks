using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRestartFeedback : GameFeedback
{
    [SerializeField] private bool _restartAnimation;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		gameObject.GetComponent<Unit>().StartAnimation = _restartAnimation;
		yield break;
	}
}
