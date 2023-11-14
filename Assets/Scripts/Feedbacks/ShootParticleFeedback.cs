using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticleFeedback : GameFeedback
{
	[SerializeField] private GameObject _particleEffectPrefab;

	public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
	{
		GameObject go = GameObject.Instantiate(_particleEffectPrefab, gameObject.GetComponent<Unit>().ShootPoint.position, gameObject.GetComponent<Unit>().ShootPoint.rotation);
		yield break;
	}
}
