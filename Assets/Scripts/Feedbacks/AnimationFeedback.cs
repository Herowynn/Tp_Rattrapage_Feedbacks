using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFeedback : GameFeedback
{
    [SerializeField] private Sprite _frame;

    public override IEnumerator Execute(GameEvent gameEvent, GameObject gameObject)
    {
        /*Debug.Log(gameEvent.name + " " + gameObject.GetComponent<Unit>().CurrentState.ToString());

        if(gameEvent.name == gameObject.GetComponent<Unit>().CurrentState.ToString())*/
		    gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _frame;

		yield break;
    }
}
