using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
[Serializable]
public class GameEvent : ScriptableObject
{
	public GameObject GameObject;

	[SerializeReference] public List<GameFeedback> Feedbacks = new List<GameFeedback>();

	public IEnumerator Execute(GameObject gameObject)
	{
		foreach(var item in Feedbacks)
		{
			yield return item.Execute(this, gameObject);
		}
	}
}
