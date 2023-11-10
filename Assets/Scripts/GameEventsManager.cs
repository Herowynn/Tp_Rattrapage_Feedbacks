using System.Collections;
using System.Collections.Generic;
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
			_events.Add(gameEvent.Name, gameEvent);
		}
	}

	public static void PlayEvent(string eventName, GameObject gameObject)
	{
		_events[eventName].GameObject = gameObject;
		_events[eventName].Execute();
	}
}
