using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance;

    public List<GameEvent> GameEvents;

    private static Dictionary<string, GameEvent> _events;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}

		_events = new Dictionary<string, GameEvent>(GameEvents.Count);

		foreach(var gameEvent in GameEvents)
		{
			Debug.Log(gameEvent.name);
			_events.Add(gameEvent.name, gameEvent);
		}
	}

	public void PlayEvent(string eventName, GameObject gameObject)
	{
		_events[eventName].GameObject = gameObject;
		StartCoroutine(_events[eventName].Execute(gameObject));
	}
}
