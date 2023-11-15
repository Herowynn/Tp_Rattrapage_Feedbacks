using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeFeedback : GameFeedback
{
	[SerializeField] private float _shakeFactor;
	[SerializeField] private bool _isShaking;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		gameObject.GetComponent<CameraShake>().ShakeFactor = _shakeFactor;
		gameObject.GetComponent<CameraShake>().IsShaking = _isShaking;
		yield break;
	}
}
